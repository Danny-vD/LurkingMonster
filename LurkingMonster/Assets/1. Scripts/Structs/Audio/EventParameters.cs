using System.Collections.Generic;

namespace Structs.Audio
{
	public readonly struct EventParameters
	{
		public readonly Dictionary<string, float> ParameterValues;

		public EventParameters(params KeyValuePair<string, float>[] values)
		{
			ParameterValues = new Dictionary<string, float>();
			
			foreach (KeyValuePair<string, float> pair in values)
			{
				ParameterValues.Add(pair.Key, pair.Value);
			}
		}
		
		public void AddParameter(string parameterName, float parameterValue)
		{
			ParameterValues.Add(parameterName, parameterValue);
		}

		public void ChangeParameterValue(string parameterName, float parameterValue)
		{
			ParameterValues[parameterName] = parameterValue;
		}

		public void RemoveParameter(string parameterName)
		{
			ParameterValues.Remove(parameterName);
		}

		public float GetParameterValue(string parameterName)
		{
			return ParameterValues[parameterName];
		}
	}
}