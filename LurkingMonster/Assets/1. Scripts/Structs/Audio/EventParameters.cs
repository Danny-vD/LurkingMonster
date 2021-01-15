using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Structs.Audio
{
	[Serializable]
	public struct EventParameters
	{
		[SerializeField]
		private SerializableDictionary<string, float> parameters;

		public Dictionary<string, float> Parameters => new Dictionary<string, float>(parameters);
		
		public EventParameters(params KeyValuePair<string, float>[] values)
		{
			parameters = new Dictionary<string, float>();
			
			foreach (KeyValuePair<string, float> pair in values)
			{
				parameters.Add(pair.Key, pair.Value);
			}
		}
		
		public void AddParameter(string parameterName, float parameterValue)
		{
			parameters.Add(parameterName, parameterValue);
		}

		public void ChangeParameterValue(string parameterName, float parameterValue)
		{
			parameters[parameterName] = parameterValue;
		}

		public void RemoveParameter(string parameterName)
		{
			parameters.Remove(parameterName);
		}

		public float GetParameterValue(string parameterName)
		{
			return parameters[parameterName];
		}
	}
}