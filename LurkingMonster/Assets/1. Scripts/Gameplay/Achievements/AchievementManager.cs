using System.Collections.Generic;
using Enums;
using Events;
using Events.BuildingEvents;
using Events.MoneyManagement;
using Singletons;
using Structs;
using UI;
using UnityEngine;
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
		private Achievement buyPlotsAchievement;
		private Achievement upgradeBuildingAchievement;

		private List<Achievement> achievements;

		private void Start()
		{
			achievements = new List<Achievement>();

			buildingBuildAchievement = new Achievement(new[] {3, 7, 10}, "BUILDINGSBUILDACHIEVEMENT",
				new object[] {SoilType.Loam, PowerUpType.AvoidMonster, FoundationType.Shallow_Foundation}, "ACHIEVEMENT_1");

			rentCollectedAchievement = new Achievement(new[] {1000, 10000, 100000}, "RENTCOLLECTEDACHIEVEMENT",
				new object[] {FoundationType.Floating_Floor_Plate, SoilType.Clay, FoundationType.Reinforced_Concrete}, "ACHIEVEMENT_2");

			buildingSavedAchievement = new Achievement(new[] {10, 20, 30}, "BUILDINGSAVEDACHIEVEMENT",
				new object[] {SoilType.Peat, PowerUpType.FixProblems, SoilType.Sand}, "ACHIEVEMENT_3");

			buyPlotsAchievement = new Achievement(new[] {5, 10, 15}, "BUYPLOTACHIEVEMENT",
				new object[] {PowerUpType.AvoidMonster, PowerUpType.AvoidMonster, BuildingType.Apartment}, "ACHIEVEMENT_4");

			upgradeBuildingAchievement = new Achievement(new[] {1, 3, 6}, "UPGRADEBUILDINGSACHIEVEMENT",
				new object[] {PowerUpType.AvoidWeatherEvent, FoundationType.Wooden_Poles, BuildingType.Store}, "ACHIEVEMENT_5");

			//TODO: make seperate system where it activates a weatherEvent when conditions are met?
			//buildingConsumedAchievement = new Achievement(new[] {5, 10, 20}, "BUILDINGCONSUMEDACHIEVEMENT",
			//	new object[] {BuildingType.Store, PowerUpType.AvoidMonster, PowerUpType.AvoidWeatherEvent}, "ACHIEVEMENT_6");

			achievements.Add(buildingBuildAchievement);
			achievements.Add(rentCollectedAchievement);
			achievements.Add(buildingSavedAchievement);
			achievements.Add(buyPlotsAchievement);
			achievements.Add(upgradeBuildingAchievement);

			//achievements.Add(buildingConsumedAchievement);

			AddListeners();

			if (UserSettings.SettingsExist)
			{
				LoadData();
			}
		}

		private void AddListeners()
		{
			CollectRentEvent.Listeners                   += OnRentCollectListener;
			BuildingBuildEvent.ParameterlessListeners    += OnBuildingBuildListener;
			BuildingSavedEvent.ParameterlessListeners    += OnBuildingsSavedListener;
			BuildingConsumedEvent.ParameterlessListeners += OnBuildingsConsumedListener;
			BuyPlotEvent.ParameterlessListeners          += OnAmountOfPlotsListener;
			BuildingUpgradeEvent.ParameterlessListeners  += OnBuildingDestroyedListener;

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
			//buildingConsumedAchievement.CheckAchievement(1);
		}

		private void OnAmountOfPlotsListener()
		{
			buyPlotsAchievement.CheckAchievement(1);
		}

		private void OnBuildingDestroyedListener()
		{
			upgradeBuildingAchievement.CheckAchievement(1);
		}

		private void OnDestroy()
		{
			UserSettings.OnGameQuit -= SaveData;

			if (!EventManager.IsInitialized) return;

			CollectRentEvent.Listeners                   -= OnRentCollectListener;
			BuildingBuildEvent.ParameterlessListeners    -= OnBuildingBuildListener;
			BuildingSavedEvent.ParameterlessListeners    -= OnBuildingsSavedListener;
			BuildingConsumedEvent.ParameterlessListeners -= OnBuildingsConsumedListener;
			BuyPlotEvent.ParameterlessListeners          -= OnAmountOfPlotsListener;
			BuildingUpgradeEvent.ParameterlessListeners  -= OnBuildingDestroyedListener;
		}
	}
}