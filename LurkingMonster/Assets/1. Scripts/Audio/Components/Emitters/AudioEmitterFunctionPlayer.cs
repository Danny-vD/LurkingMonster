using Enums;
using Enums.Audio;
using UnityEngine;

namespace Audio.Components.Emitters
{
	public class AudioEmitterFunctionPlayer : AbstractFunctionAudioHandler
	{
		[SerializeField]
		private EmitterType emitterType;
		
		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			AudioPlayer.PlayEmitter(emitterType);
		}
	}
}