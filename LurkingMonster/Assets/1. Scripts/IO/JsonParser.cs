using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using VDFramework;

// ReSharper disable ConvertToNullCoalescingCompoundAssignment
// Reason: Unity does not support it

namespace IO
{
    public class JsonParser : BetterMonoBehaviour
    {
        [Tooltip("The name of the .json file")]
        public string FileName = "Language.json";

        public static string FileContent { get; private set; }

        private void Awake()
        {
            string pathToFile = Application.streamingAssetsPath + "/" + FileName;
            FileContent = File.ReadAllText(pathToFile);
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
            get { return instance = instance ?? JsonUtility.FromJson<JsonVariables>(JsonParser.FileContent); }
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
            return entryPerVariable =
                entryPerVariable ?? CalculateDictionary.GetNestedDictionary(Variables);
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