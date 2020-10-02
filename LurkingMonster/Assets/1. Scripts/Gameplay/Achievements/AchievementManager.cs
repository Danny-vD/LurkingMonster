using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
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
		
		[SerializeField]
		private int buildingCounter = 0;
		
		[SerializeField]
		private int rentCollected = 0;
		
		[SerializeField]
		private int buildingsSaved = 0;
		
		[SerializeField]
		private int buildingsConsumed = 0;
		
		[SerializeField]
		private int amountOfPlots = 0;

		public static LevelBar LevelBar;
		
		private Achievement buildingBuildAchievement;
		private Achievement rentCollectedAchievement;
		private Achievement buildingSavedAchievement;
		private Achievement buildingConsumedAchievement;
		private Achievement amountOfPlotsAchievement;

		private List<Achievement> achievements;

		private void Start()
		{
			Achievement.LevelBar        = GetComponentInChildren<LevelBar>();
			achievements                = new List<Achievement>();
			
			
			buildingBuildAchievement    = new Achievement(new int[] {75, 150, 200}, new int[] {5, 10, 15}, "Buildings build");
			rentCollectedAchievement    = new Achievement(new int[] {100, 500, 1000, 2000}, new int[] {1000, 10000, 100000, 200000}, "Rent collected");
			buildingSavedAchievement    = new Achievement(new int[] {50, 100, 200}, new int[] {10, 20, 30}, "Buildings saved");
			buildingConsumedAchievement = new Achievement(new int[] {100, 200, 300}, new int[] {5, 10, 20}, "Buildings consumed");
			amountOfPlotsAchievement    = new Achievement(new int[] {75, 150, 200}, new int[] {5, 10, 20}, "Plots build");
			
			achievements.Add(buildingBuildAchievement);
			achievements.Add(rentCollectedAchievement);
			achievements.Add(buildingSavedAchievement);
			achievements.Add(buildingConsumedAchievement);
			achievements.Add(amountOfPlotsAchievement);
			
			AddListeners();
		}

		private void AddListeners()
		{
			EventManager.Instance.AddListener<BuildingBuildEvent>(OnBuildingBuildListener);
			EventManager.Instance.AddListener<CollectRentEvent>(OnRentCollectListener);
			EventManager.Instance.AddListener<BuildingSavedEvent>(OnBuildingsSavedListener);
			EventManager.Instance.AddListener<BuildingConsumedEvent>(OnBuildingsConsumedListener);
			EventManager.Instance.AddListener<AmountOfPlotsEvent>(OnAmountOfPlotsListener);
		}

		public void ShowAchievementProgress()
		{
			achievementParent.DestroyChildren();
			
			for (int i = 0; i < achievements.Count; i++)
			{
				GameObject prefabAchievement = Instantiate(prefabSingleAchievement, achievementParent);
				achievements[i].PrintAchievementProgress(prefabAchievement, buildingCounter);
			}
		}
		
		private void OnBuildingBuildListener()
		{
			buildingCounter++;
			buildingBuildAchievement.CheckAchievement(buildingCounter);
		}

		private void OnRentCollectListener(CollectRentEvent collectEvent)
		{
			rentCollected += collectEvent.Rent;
			rentCollectedAchievement.CheckAchievement(rentCollected);
		}
		
		private void OnBuildingsSavedListener()
		{
			buildingsSaved++;
			buildingSavedAchievement.CheckAchievement(buildingsSaved);
		}

		private void OnBuildingsConsumedListener()
		{
			buildingsConsumed++;
			buildingConsumedAchievement.CheckAchievement(buildingsConsumed);
		}

		private void OnAmountOfPlotsListener()
		{
			amountOfPlots++;
			amountOfPlotsAchievement.CheckAchievement(amountOfPlots);
		}
	}
}