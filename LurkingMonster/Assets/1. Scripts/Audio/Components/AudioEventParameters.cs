﻿using System.Collections.Generic;
using Enums;
using FMOD.Studio;
using Structs.Audio;
using UnityEngine;
using Utility;

namespace Audio.Components
{
	public class AudioEventParameters : AbstractEventReactor
	{
		//TODO: find a way to serialize the parameters
		[SerializeField]
		private SerializableEnumDictionary<UnityFunction, EventParameters> parameters;

		[SerializeField]
		private AudioEventPlayer audioEventPlayer;

		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			EventInstance eventInstance = audioEventPlayer.GetInstance;

			foreach (KeyValuePair<string, float> pair in parameters[unityFunction].ParameterValues)
			{
				eventInstance.setParameterByName(pair.Key, pair.Value);
			}
		}

		private void Reset()
		{
			audioEventPlayer = GetComponent<AudioEventPlayer>();
		}
	}
}