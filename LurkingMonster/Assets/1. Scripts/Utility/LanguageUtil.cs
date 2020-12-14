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
			//Building types
			{
				BuildingType.House, "HOUSE"
			},
			{
				BuildingType.Store, "STORE"
			},
			{
				BuildingType.ApartmentBuilding, "APARTMENT"
			},
			
			//Foundation types
			{
				FoundationType.Wooden_Poles, "WOODEN_POLES"
			},
			{
				FoundationType.Reinfored_Concrete, "REINFORCED_CONCRETE"
			},
			{
				FoundationType.Concrete_On_Steel, "SHALLOW_FOUNDATION"
			},
			{
				FoundationType.Floating_Floor_Plate, "FLOATING_FLOOR_PLATE"
			},
			
			//Soil types
			{
				SoilType.Sandy_Clay, "LOAM"
			},
			{
				SoilType.Peat, "PEAT"
			},
			{
				SoilType.Clay, "CLAY"
			},
			{
				SoilType.Sand, "SAND"
			},
			
			//PowerUps types
			{
				PowerUpType.AvoidMonster, "POWERUP_MEAT"
			},
			{
				PowerUpType.FixProblems, "POWERUP_FIX_PROBLEMS"
			},
			{
				PowerUpType.AvoidWeatherEvent, "POWERUP_WEATHER"
			}
		};  
		
		/// <summary>
		/// Returns the string for a specific key for the current language
		/// </summary>
		public static string GetJsonString(string variableName)
		{
			return JsonParser.Instance.GetVariable(variableName, LanguageSettings.Language.ToString());
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