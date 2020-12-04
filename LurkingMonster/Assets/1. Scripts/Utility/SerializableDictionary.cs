using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Structs.Utility;
using UnityEngine;

namespace Utility
{
	/// <summary>
	/// A 'fake' dictionary that can be serialized
	/// </summary>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, /*ICollection<SerializableKeyValuePair<TKey, TValue>>,*/
		IEnumerable<SerializableKeyValuePair<TKey, TValue>>, ISerializationCallbackReceiver
	{
		#region Fields

		[SerializeField, UsedImplicitly]
		private bool distinctKeys;
		
		[SerializeField]
		protected List<SerializableKeyValuePair<TKey, TValue>> InternalList = new List<SerializableKeyValuePair<TKey, TValue>>();
		
		[SerializeField]
		private List<SerializableKeyValuePair<TKey, TValue>> serializedList = new List<SerializableKeyValuePair<TKey, TValue>>();

		#endregion

		#region Operators

		public static implicit operator SerializableDictionary<TKey, TValue>(List<SerializableKeyValuePair<TKey, TValue>> list)
		{
			return new SerializableDictionary<TKey, TValue>(list);
		}

		public static implicit operator SerializableDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			return new SerializableDictionary<TKey, TValue>(dictionary);
		}

		#endregion

		#region Properties

		public TValue this[TKey key]
		{
			get
			{
				TryGetValue(key, out TValue value);
				return value;
			}
			set => Add(key, value);
		}

		public int Count => InternalList.Count;
		public bool IsReadOnly => false;
		
		public ICollection<TKey> Keys => InternalList.Select(pair => pair.Key).ToArray();
		public ICollection<TValue> Values => InternalList.Select(pair => pair.Value).ToArray();

		#endregion

		#region Constructors

		public SerializableDictionary()
		{
		}

		public SerializableDictionary(IEnumerable<SerializableKeyValuePair<TKey, TValue>> list)
		{
			InternalList = list.Distinct().ToList();
		}

		public SerializableDictionary(params SerializableKeyValuePair<TKey, TValue>[] keyValuePairs) : this(keyValuePairs.Distinct())
		{
		}

		public SerializableDictionary(Dictionary<TKey, TValue> dictionary) : this()
		{
			foreach (KeyValuePair<TKey, TValue> pair in dictionary)
			{
				Add(pair.Key, pair.Value);
			}
		}

		#endregion

		#region IDictionary<>

		public void Add(TKey key, TValue value)
		{
			int index = FindPair(key);

			// FindIndex returns -1 if it's not present
			if (index < 0)
			{
				InternalList.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
				return;
			}

			SerializableKeyValuePair<TKey, TValue> pair = InternalList[index];
			pair.Value = value;

			InternalList[index] = pair;
		}

		public bool Remove(TKey key)
		{
			int index = FindPair(key);

			if (index < 0)
			{
				return false;
			}

			InternalList.RemoveAt(index);
			return true;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			int index = FindPair(key);

			if (index >= 0)
			{
				value = InternalList[index].Value;
				return true;
			}

			value = default;
			return false;
		}

		#endregion

		#region ICollection

		public void Add(SerializableKeyValuePair<TKey, TValue> item)
		{
			if (!InternalList.Contains(item))
			{
				InternalList.Add(item);
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Add((SerializableKeyValuePair<TKey, TValue>) item);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return InternalList.Remove(item);
		}

		public void Clear()
		{
			InternalList.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item) => InternalList.Contains(item);

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			ToKeyValuePair().ToList().CopyTo(array, arrayIndex);
		}

		#endregion

		#region ISerializationCallbackReceiver

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			InternalList = serializedList.Distinct().ToList();

			distinctKeys = InternalList.Count == serializedList.Count;
		}

		#endregion

		public bool ContainsKey(TKey key)
		{
			return InternalList.Any(pair => pair.Key.Equals(key));
		}

		public bool ContainsValue(TValue value)
		{
			return InternalList.Any(pair => pair.Value.Equals(value));
		}

		#region IEnumerable

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			ToKeyValuePair().GetEnumerator();

		public IEnumerator<SerializableKeyValuePair<TKey, TValue>> GetEnumerator() => InternalList.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion

		#region Private

		private static void VerifyKey(object key)
		{
			switch (key)
			{
				case null:
					throw new ArgumentNullException(nameof(key), "null is not allowed for a key");
				case TKey _:
					return;
				default:
					throw new ArgumentException($"{key} is not of type {typeof(TKey).Name}", nameof(key));
			}
		}

		private static void VerifyValue(object value)
		{
			switch (value)
			{
				case TValue _:
					return;
				case null when typeof(TValue).IsValueType:
					throw new ArgumentNullException(nameof(value), $"null while {typeof(TValue).Name} cannot be null");
				default:
					throw new ArgumentException($"{value} is not of type {typeof(TValue).Name}", nameof(value));
			}
		}

		private IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePair()
		{
			return InternalList.Select(pair => (KeyValuePair<TKey, TValue>) pair);
		}

		private SerializableKeyValuePair<TKey, TValue> GetPair(TKey key)
		{
			return InternalList.First(pair => pair.Key.Equals(key));
		}

		private int FindPair(TKey key)
		{
			int index = InternalList.FindIndex(pair => pair.Key.Equals(key));
			return index;
		}

		#endregion
	}
}