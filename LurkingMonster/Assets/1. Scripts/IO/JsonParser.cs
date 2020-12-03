using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VDFramework.Singleton;

namespace IO
{
	public class JsonParser : Singleton<JsonParser>
	{
		private JsonVariables variables;

		protected override void Awake()
		{
			variables = new JsonVariables();
			
			foreach (TextAsset file in Resources.LoadAll<TextAsset>("Language"))
			{
				variables.AddVariables(JsonUtility.FromJson<JsonVariables>(file.ToString()));
			}
		}

		public string GetVariable(string variableName, string keyName)
		{
			return variables.GetVariable(variableName, keyName);
		}
	}

	[Serializable]
	public class JsonVariables
	{
		public List<Variable> Variables = new List<Variable>();

		public void AddVariables(JsonVariables jsonVariables)
		{
			Variables.AddRange(jsonVariables.Variables);
		}

		public string GetVariable(string variableName, string keyName)
		{
			try
			{
				return GetEntry()[variableName][keyName];
			}
			catch (KeyNotFoundException)
			{
				return "UNDEFINED";
			}
		}
		
		private Dictionary<string, Dictionary<string, string>> entryPerVariable = null;

		private Dictionary<string, Dictionary<string, string>> GetEntry()
		{
			return entryPerVariable = entryPerVariable ?? CalculateDictionary.GetNestedDictionary(Variables);
		}
	}

	[Serializable]
	public class KeyValuePair
	{
		public string Key;
		public string Value;
	}

	[Serializable]
	public class Variable
	{
		public string Key;
		public KeyValuePair[] Value;

		private Dictionary<string, string> dictionary = null;

		public Dictionary<string, string> GetDictionary
		{
			get { return dictionary = dictionary ?? CalculateDictionary.GetDictionary(Value); }
		}
	}

	public static class CalculateDictionary
	{
		public static Dictionary<string, string> GetDictionary(IEnumerable<KeyValuePair> pArray)
		{
			return pArray.ToDictionary(entry => entry.Key, entry => entry.Value);
		}

		public static Dictionary<string, Dictionary<string, string>> GetNestedDictionary(IEnumerable<Variable> pArray)
		{
			return pArray.ToDictionary(entry => entry.Key, entry => entry.GetDictionary);
		}
	}
}