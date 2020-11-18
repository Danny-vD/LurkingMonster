using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs
{
	[Serializable]
	public struct EventDataPerEventType : IKeyValuePair<WeatherEventType, WeatherEventData>
	{
		[SerializeField]
		private WeatherEventType key;
		
		[SerializeField]
		private WeatherEventData value;

		public bool Equals(IKeyValuePair<WeatherEventType, WeatherEventData> other)
		{
			return Key.Equals(other.Key);
		}

		public WeatherEventType Key
		{
			get => key;
			set => key = value;
		}

		public WeatherEventData Value
		{
			get => value;
			set => this.value = value;
		}
	}
}