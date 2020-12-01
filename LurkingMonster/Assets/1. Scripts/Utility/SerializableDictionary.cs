using System;
using System.Collections.Generic;
using UnityEngine;
using VDFramework.Interfaces;

namespace Utility
{
	/// <summary>
	/// A 'fake' dictionary that can be serialized
	/// </summary>
	[Serializable]
	public class SerializableDictionary<TKey, TValue>
	{
		[SerializeField]
		private List<KeyValuePair> internalList;
		
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

			public bool Equals(IKeyValuePair<TKey, TValue> other) => Key.Equals(other.Key);
		}
	}
}