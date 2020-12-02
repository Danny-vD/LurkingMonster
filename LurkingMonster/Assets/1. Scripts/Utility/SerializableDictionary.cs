using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VDFramework.Interfaces;
using VDFramework.Utility;

namespace Utility
{
	/// <summary>
	/// A 'fake' dictionary that can be serialized
	/// </summary>
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : SerializableDictionary,
		IEnumerable<SerializableDictionary<TKey, TValue>.KeyValuePair>
	{
		[SerializeField]
		public List<KeyValuePair> internalList = new List<KeyValuePair>();

		public TValue this[TKey key]
		{
			get => GetValue(key);
			set => SetValue(key, value);
		}

		public static implicit operator SerializableDictionary<TKey, TValue>(List<KeyValuePair> list)
		{
			return new SerializableDictionary<TKey, TValue>(list);
		}
		
		public SerializableDictionary()
		{
		}

		public SerializableDictionary(IEnumerable<KeyValuePair> list)
		{
			internalList = list.Distinct().ToList();
		}

		public SerializableDictionary(params KeyValuePair[] keyValuePairs) : this(keyValuePairs.Distinct())
		{
		}
		
		public void SetValue(TKey key, TValue value)
		{
			int index = internalList.FindIndex(listItem => listItem.Key.Equals(key));

			// FindIndex returns -1 if it's not present
			if (index < 0)
			{
				internalList.Add(new KeyValuePair(key, value));
				return;
			}
			
			KeyValuePair pair = internalList[index];
			pair.Value = value;

			internalList[index] = pair;
		}

		public TValue GetValue(TKey key)
		{
			return GetKeyValuePair(key).Value;
		}

		private KeyValuePair GetKeyValuePair(TKey key)
		{
			return internalList.First(pair => pair.Key.Equals(key));
		}

		public IEnumerator<KeyValuePair> GetEnumerator() => internalList.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		
		[Serializable]
		public struct KeyValuePair : IKeyValuePair<TKey, TValue>
		{
			[SerializeField]
			private TKey key;

			[SerializeField]
			private TValue value;

			public TKey Key
			{
				get => key;
				set => key = value;
			}

			public TValue Value
			{
				get => value;
				set => this.value = value;
			}

			public KeyValuePair(TKey pairKey, TValue pairValue)
			{
				key   = pairKey;
				value = pairValue;
			}

			public bool Equals(IKeyValuePair<TKey, TValue> other) => other != null && Key.Equals(other.Key);
		}
	}

	[Serializable]
	public class SerializableEnumDictionary<TKey, TValue> : SerializableEnumDictionary
		where TKey : struct, Enum
	{
		[SerializeField]
		private List<SerializableDictionary<TKey, TValue>.KeyValuePair> internalList;

		/// <summary>
		/// Automatically fills the dictionary with an entry for every enum value
		/// </summary>
		public void Populate()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<SerializableDictionary<TKey, TValue>.KeyValuePair, TKey, TValue>(internalList);
		}
	}

	/// <summary>
	/// A placeholder class to be able to create a property drawer for the SerializableDictionary
	/// </summary>
	[Serializable]
	public abstract class SerializableDictionary
	{
	}

	/// <summary>
	/// A placeholder class to be able to create a property drawer for the serializableEnumDictionary
	/// </summary>
	[Serializable]
	public abstract class SerializableEnumDictionary
	{
	}
}