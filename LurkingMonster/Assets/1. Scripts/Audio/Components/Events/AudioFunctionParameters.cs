using System.Collections.Generic;
using Enums;
using FMOD.Studio;
using Structs.Audio;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Audio.Components.Events
{
	public class AudioFunctionParameters : AbstractFunctionAudioHandler
	{
		[SerializeField]
		private AudioFunctionPlayer audioFunction;
		
		[SerializeField]
		private SerializableEnumDictionary<UnityFunction, EventParameters> parameters;

		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			EventInstance eventInstance = audioFunction.GetInstance;

			foreach (KeyValuePair<string, float> pair in parameters[unityFunction].Parameters)
			{
				eventInstance.setParameterByName(pair.Key, pair.Value);
			}
		}

		private void Reset()
		{
			audioFunction = GetComponent<AudioFunctionPlayer>();
		}
	}
}