using Enums;
using Enums.Audio;
using UnityEngine;
using Utility;

namespace Audio.Components
{
	public class AudioEventEmitterStopper : AbstractEventReactor
	{
		[SerializeField]
		private EmitterType emitterType;
		
		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			AudioPlayer.StopEmitter(emitterType);
		}
	}
}