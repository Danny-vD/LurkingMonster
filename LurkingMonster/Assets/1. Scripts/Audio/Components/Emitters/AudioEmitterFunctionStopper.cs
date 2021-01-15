using Enums;
using Enums.Audio;
using UnityEngine;

namespace Audio.Components.Emitters
{
	public class AudioEmitterFunctionStopper : AbstractFunctionAudioHandler
	{
		[SerializeField]
		private EmitterType emitterType;
		
		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			AudioPlayer.StopEmitter(emitterType);
		}
	}
}