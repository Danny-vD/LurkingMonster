using Enums;
using FMOD.Studio;
using UnityEngine;

namespace Audio.Components.Events
{
	public class AudioEventStopper : AbstractFunctionAudioHandler
	{
		[SerializeField]
		private AudioEventPlayer audioEventPlayer;
		
		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			EventInstance eventInstance = audioEventPlayer.GetInstance;

			eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}

		private void Reset()
		{
			audioEventPlayer = GetComponent<AudioEventPlayer>();
		}
	}
}