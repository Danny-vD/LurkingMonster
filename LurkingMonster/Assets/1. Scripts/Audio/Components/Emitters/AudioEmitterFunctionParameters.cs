using System.Collections.Generic;
using Enums;
using Enums.Audio;
using FMODUnity;
using Structs.Audio;
using UnityEngine;
using Utility;

namespace Audio.Components.Emitters
{
	public class AudioEmitterFunctionParameters : AbstractFunctionAudioHandler
	{
		[SerializeField]
		private EmitterType emitterType;
		
		[SerializeField]
		private SerializableEnumDictionary<UnityFunction, EventParameters> parameters;

		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			StudioEventEmitter emitter = AudioManager.Instance.EventPaths.GetEmitter(emitterType);

			foreach (KeyValuePair<string, float> pair in parameters[unityFunction].Parameters)
			{
				emitter.SetParameter(pair.Key, pair.Value);
			}
		}
	}
}