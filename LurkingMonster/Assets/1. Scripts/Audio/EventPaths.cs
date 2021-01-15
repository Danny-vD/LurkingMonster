using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Audio;
using Structs.Audio;
using FMODUnity;
using UnityEngine;
using VDFramework.Extensions;
using VDFramework.Utility;
using EventType = Enums.Audio.EventType;

namespace Audio
{
	[Serializable]
	public class EventPaths
	{
		private const string masterBusPath = "Bus:/";
		
		[SerializeField]
		private List<EventPathPerEvent> events = new List<EventPathPerEvent>();

		[SerializeField]
		private List<BusPathPerBus> buses = new List<BusPathPerBus>();

		[SerializeField]
		private List<EventsPerEmitter> emitterEvents = new List<EventsPerEmitter>();

		private readonly Dictionary<EmitterType, StudioEventEmitter> emitters =
			new Dictionary<EmitterType, StudioEventEmitter>();

		public EventPaths()
		{
			buses.Add(new BusPathPerBus {Key = BusType.Master, Value = masterBusPath});
		}

		public void UpdateDictionaries()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<EventPathPerEvent, EventType, string>(events);

			EnumDictionaryUtil.PopulateEnumDictionary<BusPathPerBus, BusType, string>(buses);

			EnumDictionaryUtil.PopulateEnumDictionary<EventsPerEmitter, EmitterType, EventType>(emitterEvents);
		}

		public void AddEmitters(GameObject gameObject)
		{
			foreach (EmitterType emitterType in default(EmitterType).GetValues())
			{
				StudioEventEmitter emitter = gameObject.AddComponent<StudioEventEmitter>();
				emitter.Event = GetPathForEmitter(emitterType);

				emitters.Add(emitterType, emitter);
			}
		}

		public string GetPath(EventType eventType)
		{
			return events.First(item => item.Key.Equals(eventType)).Value;
		}

		public string GetPath(BusType busType)
		{
			return buses.First(item => item.Key.Equals(busType)).Value;
		}

		public StudioEventEmitter GetEmitter(EmitterType emitterType)
		{
			return emitters[emitterType];
		}

		private string GetPathForEmitter(EmitterType emitterType)
		{
			EventType eventType = emitterEvents.First(item => item.Key == emitterType).Value;
			return GetPath(eventType);
		}
	}
}