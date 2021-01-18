using Enums;
using FMOD.Studio;
using Interfaces;
using UnityEngine;
using EventType = Enums.Audio.EventType;

namespace Audio.Components.Events
{
	public class AudioFunctionPlayer : AbstractFunctionAudioHandler, IAudioplayer
	{
		[SerializeField]
		private EventType eventType;
		
		private EventInstance eventInstance;

		public EventInstance GetInstance => eventInstance;

		protected override void Start()
		{
			eventInstance = AudioPlayer.GetEventInstance(eventType);
			base.Start();
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