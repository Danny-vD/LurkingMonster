using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Events;
using Events.Achievements;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Singleton;

namespace Singletons
{
	public class RewardManager : Singleton<RewardManager>
	{
		//Save houses from monster 5 = unlock peet/moor, 10 x = Avoid weather for 10 mins, 30 x = Unlock sand
		private Dictionary<SoilType, bool> soilReward;
		private Dictionary<FoundationType, bool> foundationReward;
		private Dictionary<BuildingType, bool> buildingReward;

		private int counter = 0;

		protected override void Awake()
		{
			soilReward = new Dictionary<SoilType, bool>();
			foundationReward = new Dictionary<FoundationType, bool>();
			buildingReward = new Dictionary<BuildingType, bool>();
			
			AddSoilTypes();
			AddFoundationTypes();
			AddBuildingTypes();
			
			if (UserSettings.SettingsExist)
			{
				counter = UserSettings.GameData.AchievementCounter;
			}
			
			UserSettings.OnGameQuit += SaveData;
		}

		private void SaveData()
		{
			UserSettings.GameData.AchievementCounter = counter;
		}

		public bool IsUnlocked(object obj)
		{
			switch (obj)
			{
				case SoilType type1:
					return soilReward[type1];
				case FoundationType type2:
					return foundationReward[type2];
				case BuildingType type3:
					return buildingReward[type3];
				default:
					throw new Exception("Type not found in reward manager " + obj);
			}
		}
		
		public void Unlock(object obj)
		{
			switch (obj)
			{
				case SoilType type1:
					soilReward[type1] = true;
					break;
				case FoundationType type2:
					foundationReward[type2] = true;
					break;
				case BuildingType type3:
					buildingReward[type3] = true;
					break;
				case PowerUpType type4:
					EventManager.Instance.RaiseEvent(new PowerUpIncreaseEvent(type4));
					break;
			}
			
			counter--;
			EventManager.Instance.RaiseEvent(new AchievementUnlockedEvent());
			CheckIfAllAchievementsUnlocked();
		}

		public void IncreaseCounter()
		{
			counter++;
		}

		public void FinishAllAchievements()
		{
			EventManager.Instance.RaiseEvent(new EndGameEvent());
		}

		private void CheckIfAllAchievementsUnlocked()
		{
			if (soilReward.All(pair => pair.Value) &&
				foundationReward.All(pair => pair.Value) &&
				buildingReward.All(pair => pair.Value))
			{
				FinishAllAchievements();
			}
		}

		private void Print(object obj)
		{
			foreach (KeyValuePair<SoilType, bool> soil in soilReward)
			{
				print(soil.Key + " " + soil.Value);
			}
			
			foreach (KeyValuePair<FoundationType, bool> foundation in foundationReward)
			{
				print(foundation.Key + " " + foundation.Value);
			}
		}

		private void AddSoilTypes()
		{
			foreach (SoilType soilType in default(SoilType).GetValues())
			{
				soilReward.Add(soilType, false);
			}
		}

		private void AddFoundationTypes()
		{
			foreach (FoundationType foundationType in default(FoundationType).GetValues())
			{
				foundationReward.Add(foundationType, false);
			}
		}

		private void AddBuildingTypes()
		{
			foreach (BuildingType buildingType in default(BuildingType).GetValues())
			{
				buildingReward.Add(buildingType, false);
			}	
		}

		public int Counter => counter;
	}
}