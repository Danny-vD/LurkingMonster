using System;
using System.Collections.Generic;
using Enums;
using IO;

namespace Utility
{
	public static class LanguageUtil
	{
		private static Dictionary<object, string> rewardInfo = new Dictionary<object, string>()
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
		
		public static string GetJsonString(string variableName)
		{
			return JsonVariables.Instance.GetVariable(variableName, LanguageSettings.Language.ToString());
		}

		public static string GetRewardInfo(object @object)
		{
			if (rewardInfo.ContainsKey(@object))
			{
				return rewardInfo[@object];
			}
			
			throw new Exception("Reward info does not exist " + @object);
		}
	}
}