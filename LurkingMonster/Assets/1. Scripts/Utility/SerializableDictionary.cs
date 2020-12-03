using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Structs.Utility;
using UnityEngine;
using VDFramework.Utility;

namespace Utility
{
	/// <summary>
	/// A 'fake' dictionary that can be serialized
	/// </summary>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : IEnumerable<SerializableKeyValuePair<TKey, TValue>>
	{
		[SerializeField]
		protected List<SerializableKeyValuePair<TKey, TValue>> internalList = new List<SerializableKeyValuePair<TKey, TValue>>();

		public static implicit operator SerializableDictionary<TKey, TValue>(List<SerializableKeyValuePair<TKey, TValue>> list)
		{
			return new SerializableDictionary<TKey, TValue>(list);
		}

		public static implicit operator SerializableDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			return new SerializableDictionary<TKey, TValue>(dictionary);
		}

		public TValue this[TKey key]
		{
			get => GetValue(key);
			set => SetValue(key, value);
		}

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
				AddValue(pair.Key, pair.Value);
			}
		}

		public void SetValue(TKey key, TValue value)
		{
			int index = internalList.FindIndex(listItem => listItem.Key.Equals(key));

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

		public TValue GetValue(TKey key)
		{
			return GetKeyValuePair(key).Value;
		}

		public void AddValue(TKey key, TValue value)
		{
			// Enforce distinct keys by only adding if it's not in the list yet
			SetValue(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			return internalList.Any(pair => pair.Key.Equals(key));
		}

		public bool ContainsValue(TValue value)
		{
			return internalList.Any(pair => pair.Value.Equals(value));
		}

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

		private SerializableKeyValuePair<TKey, TValue> GetKeyValuePair(TKey key)
		{
			return internalList.First(pair => pair.Key.Equals(key));
		}

		public IEnumerator<SerializableKeyValuePair<TKey, TValue>> GetEnumerator() => internalList.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	[Serializable]
	public class SerializableEnumDictionary<TKey, TValue> : SerializableDictionary<TKey, TValue>
		where TKey : struct, Enum
	{
		public static implicit operator SerializableEnumDictionary<TKey, TValue>(List<SerializableKeyValuePair<TKey, TValue>> list)
		{
			return new SerializableEnumDictionary<TKey, TValue>(list);
		}

		public static implicit operator SerializableEnumDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			return new SerializableEnumDictionary<TKey, TValue>(dictionary);
		}

		public SerializableEnumDictionary(IEnumerable<SerializableKeyValuePair<TKey, TValue>> list) : base(list)
		{
		}

		public SerializableEnumDictionary(params SerializableKeyValuePair<TKey, TValue>[] keyValuePairs) : base(keyValuePairs.Distinct())
		{
		}

		public SerializableEnumDictionary(Dictionary<TKey, TValue> dictionary) : base(dictionary)
		{
		}

		/// <summary>
		/// Automatically fills the dictionary with an entry for every enum value
		/// </summary>
		public void Populate()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<SerializableKeyValuePair<TKey, TValue>, TKey, TValue>(internalList);
		}
	}
}