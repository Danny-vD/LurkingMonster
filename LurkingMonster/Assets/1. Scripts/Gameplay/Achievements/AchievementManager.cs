using System;
using System.Collections.Generic;
using Enums;
using Events;
using IO;
using Singletons;
using Structs;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.UnityExtensions;

namespace Gameplay.Achievements
{
	public class AchievementManager : BetterMonoBehaviour
	{
		[SerializeField]
		private Transform achievementParent = null;

		[SerializeField]
		private GameObject prefabSingleAchievement = null;

		
		private Achievement buildingBuildAchievement;
		private Achievement rentCollectedAchievement;
		private Achievement buildingSavedAchievement;
		private Achievement buildingConsumedAchievement;
		private Achievement amountOfPlotsAchievement;
		private Achievement destroyHousesAchievement;

		private List<Achievement> achievements;

		private void Start()
		{
			achievements                = new List<Achievement>();
			
			buildingBuildAchievement    = new Achievement(new int[] {1, 10, 20}, "BUILDINGSBUILDACHIEVEMENT",
				new object[] {PowerUpType.FixProblems, SoilType.Sandy_Clay, FoundationType.Concrete_On_Steel}, "PLACEHOLDER");
			rentCollectedAchievement    = new Achievement(new int[] {1000, 10000, 100000}, "RENTCOLLECTEDACHIEVEMENT",
				new object[] {FoundationType.Floating_Floor_Plate, SoilType.Clay, FoundationType.Reinfored_Concrete}, "PLACEHOLDER");
			buildingSavedAchievement    = new Achievement(new int[] {10, 20, 30}, "BUILDINGSAVEDACHIEVEMENT",
				new object[] {SoilType.Peet, PowerUpType.FixProblems, SoilType.Sand}, "PLACEHOLDER");
			buildingConsumedAchievement = new Achievement(new int[] {5, 10, 20}, "BUILDINGCONSUMEDACHIEVEMENT",
				new object[] {RandomWeatherEventType.Earthquake, RandomWeatherEventType.Storm, RandomWeatherEventType.BuildingTunnels}, "PLACEHOLDER");
			amountOfPlotsAchievement    = new Achievement(new int[] {5, 10, 20}, "AMOUNTOFPLOTSACHIEVEMENT",
				new object[] {8, 10, BuildingType.ApartmentBuilding}, "PLACEHOLDER");
			destroyHousesAchievement    = new Achievement(new int[] {2, 5, 10}, "DESTROYHOUSESACHIEVEMENT",
				new object[] {PowerUpType.AvoidWeatherEvent, FoundationType.Wooden_Poles, BuildingType.Store}, "PLACEHOLDER");
			

			achievements.Add(buildingBuildAchievement);
			achievements.Add(rentCollectedAchievement);
			achievements.Add(buildingSavedAchievement);
			achievements.Add(buildingConsumedAchievement);
			achievements.Add(amountOfPlotsAchievement);
			achievements.Add(destroyHousesAchievement);
			
			
			AddListeners();
			
			if (UserSettings.SettingsExist)
			{
				LoadData();
			}
		}

		private void AddListeners()
		{
			//TODO Create remove listeners
			EventManager.Instance.AddListener<BuildingBuildEvent>(OnBuildingBuildListener);
			EventManager.Instance.AddListener<CollectRentEvent>(OnRentCollectListener);
			EventManager.Instance.AddListener<BuildingSavedEvent>(OnBuildingsSavedListener);
			EventManager.Instance.AddListener<BuildingConsumedEvent>(OnBuildingsConsumedListener);
			EventManager.Instance.AddListener<AmountOfPlotsEvent>(OnAmountOfPlotsListener);
			EventManager.Instance.AddListener<BuildingDestroyedEvent>(OnBuildingDestroyedListener);

			UserSettings.OnGameQuit += SaveData;
		}
		
		public void ShowAchievementProgress()
		{
			achievementParent.DestroyChildren();
			
			for (int i = 0; i < achievements.Count; i++)
			{
				GameObject prefabAchievement = Instantiate(prefabSingleAchievement, achievementParent);
				prefabAchievement.GetComponent<ProgressImageHandler>().Instantiate(achievements[i]);
				achievements[i].PrintAchievement(prefabAchievement);
			}
		}

		private void SaveData()
		{
			AchievementData[] achievementData = new AchievementData[achievements.Count];  
			
			for (int i = 0; i < achievements.Count; i++)
			{
				achievementData[i] = achievements[i].GetData();
			}

			UserSettings.GameData.AchievementData = achievementData;
		}

		private void LoadData()
		{
			AchievementData[] achievementData = UserSettings.GameData.AchievementData;
			
			for (int i = 0; i < achievements.Count; i++)
			{
				achievements[i].SetData(achievementData[i]);
			}
		}

		private void OnBuildingBuildListener()
		{
			buildingBuildAchievement.CheckAchievement(1);
		}

		private void OnRentCollectListener(CollectRentEvent collectEvent)
		{
			int rentCollected = collectEvent.Rent;
			rentCollectedAchievement.CheckAchievement(rentCollected);
		}
		
		private void OnBuildingsSavedListener()
		{
			buildingSavedAchievement.CheckAchievement(1);
		}

		private void OnBuildingsConsumedListener()
		{
			buildingConsumedAchievement.CheckAchievement(1);
		}

		private void OnAmountOfPlotsListener()
		{
			amountOfPlotsAchievement.CheckAchievement(1);
		}

		private void OnBuildingDestroyedListener()
		{
			destroyHousesAchievement.CheckAchievement(1);
		}
	}
}