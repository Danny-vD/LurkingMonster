using Enums;
using FMOD.Studio;
using Interfaces;
using UnityEngine;
using Utility;
using EventType = Enums.Audio.EventType;

namespace Audio.Components
{
	public class AudioEventPlayer : AbstractEventReactor, IAudioplayer
	{
		[SerializeField]
		private EventType eventType;
		
		private EventInstance eventInstance;

		public EventInstance GetInstance => eventInstance;

		private void Awake()
		{
			eventInstance = AudioPlayer.GetEventInstance(eventType);
		}

		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			eventInstance.start();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			eventInstance.release();
		}
	}
}