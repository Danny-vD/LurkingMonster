using System;
using System.Collections.Generic;
using Enums;
using IO;

namespace Utility
{
	public static class LanguageUtil
	{
		//TODO: Make dominique add entries for every reward possible.
		private static readonly Dictionary<object, string> rewardInfo = new Dictionary<object, string>()
		{
			{
				FoundationType.Wooden_Poles, "WOODEN_POLES"
			},
			{
				FoundationType.Reinfored_Concrete, "REINFORCED_CONCRETE"
			},
			{
				FoundationType.Concrete_On_Steel, "CONCRETE_ON_STEEL"
			}
		};  
		
		/// <summary>
		/// Returns the string for a specific key for the current language
		/// </summary>
		public static string GetJsonString(string variableName)
		{
			return JsonVariables.Instance.GetVariable(variableName, LanguageSettings.Language.ToString());
		}

		/// <summary>
		/// Returns the formatted string for a specific key for the current language  
		/// </summary>
		public static string GetJsonString(string variableName, params object[] args)
		{
			return string.Format(GetJsonString(variableName), args);
		}

		public static string GetRewardInfo(object @object)
		{
			if (rewardInfo.ContainsKey(@object))
			{
				return rewardInfo[@object];
			}

			return "No reward info available";
		}
	}
}