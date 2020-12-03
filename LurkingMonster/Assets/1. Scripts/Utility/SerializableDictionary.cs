using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Structs.Utility;
using UnityEngine;

namespace Utility
{
	/// <summary>
	/// A 'fake' dictionary that can be serialized
	/// </summary>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : IEnumerable<SerializableKeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
	{
		#region Fields

		[SerializeField]
		protected List<SerializableKeyValuePair<TKey, TValue>> internalList = new List<SerializableKeyValuePair<TKey, TValue>>();

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
			get => GetValue(key);
			set => Add(key, value);
		}

		#endregion

		#region Constructors

		public SerializableDictionary()
		{
		}

		public SerializableDictionary(IEnumerable<SerializableKeyValuePair<TKey, TValue>> list)
		{
			internalList = list.Distinct().ToList();
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
			int index = GetIndex(key);

			// FindIndex returns -1 if it's not present
			if (index < 0)
			{
				internalList.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
				return;
			}

			SerializableKeyValuePair<TKey, TValue> pair = internalList[index];
			pair.Value = value;

			internalList[index] = pair;
		}

		public bool Remove(TKey key)
		{
			int index = GetIndex(key);

			if (index < 0)
			{
				return false;
			}

			internalList.RemoveAt(index);
			return true;
		}

		#endregion

		#region ICollection
		
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (!internalList.Contains(item))
			{
				internalList.Add(item);
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return internalList.Remove(item);
		}
		
		public void Clear()
		{
			internalList.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item) => internalList.Contains(item);

		#endregion

		public TValue GetValue(TKey key)
		{
			return GetPair(key).Value;
		}

		public bool ContainsKey(TKey key)
		{
			return internalList.Any(pair => pair.Key.Equals(key));
		}

		public bool ContainsValue(TValue value)
		{
			return internalList.Any(pair => pair.Value.Equals(value));
		}

		#region IEnumerable

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
			ToKeyValuePair().GetEnumerator();

		public IEnumerator<SerializableKeyValuePair<TKey, TValue>> GetEnumerator() => internalList.GetEnumerator();

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
			return internalList.Select(pair => (KeyValuePair<TKey, TValue>) pair);
		}

		private SerializableKeyValuePair<TKey, TValue> GetPair(TKey key)
		{
			return internalList.First(pair => pair.Key.Equals(key));
		}

		private int GetIndex(TKey key)
		{
			int index = internalList.FindIndex(pair => pair.Key.Equals(key));
			return index;
		}

		#endregion
	}
}