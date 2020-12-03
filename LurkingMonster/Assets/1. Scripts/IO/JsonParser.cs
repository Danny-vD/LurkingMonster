using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VDFramework;

namespace IO
{
	public class JsonParser : BetterMonoBehaviour
	{
		// TODO: make a folder in resources that has all the language files
		// Give JsonParser a dictionary<string, JsonVariables> // key == language file name (make key optional)
		// Make json parser read every language file and perform the FromJson for every file
		// Any user can then request a JsonVariables from JsonParser (using file name) and from there get the required string

		[Tooltip("The name of the .json file")]
		public string FileName = "Language";

		public static string FileContent { get; private set; }

		private void Awake()
		{
			TextAsset file = (TextAsset) Resources.Load(FileName);
			FileContent = file.ToString();
		}
	}

	[Serializable]
	public class JsonVariables
	{
		private static JsonVariables instance = null;

		public Variable[] Variables;

		private Dictionary<string, Dictionary<string, string>> entryPerVariable = null;

		public static JsonVariables Instance
		{
			get
			{
				if (JsonParser.FileContent == null)
				{
					throw new NullReferenceException("No Json parser present in the scene");
				}

				return instance = instance ?? JsonUtility.FromJson<JsonVariables>(JsonParser.FileContent);
			}
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