using FMOD.Studio;
using UnityEngine;
using VDFramework;
using EventType = Enums.Audio.EventType;

namespace Audio.Audioplayers
{
	public class SoundPlayer : BetterMonoBehaviour
	{
		[SerializeField]
		private EventType eventType;

		private EventInstance eventInstance;

		private void Awake()
		{
			CacheEventInstance();
		}

		public void SetEventType(EventType @event)
		{
			eventType = @event;
			CacheEventInstance();
		}
		
		public void Play()
		{
			eventInstance.start();
		}

		private void CacheEventInstance()
		{
			eventInstance = AudioPlayer.GetEventInstance(eventType);
		}
	}
}