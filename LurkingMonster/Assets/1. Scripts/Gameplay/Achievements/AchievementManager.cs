using System;
using System.Collections.Generic;
using Events;
using UI;
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

		
		private Achievement buildingBuildAchievement;
		private Achievement rentCollectedAchievement;
		private Achievement buildingSavedAchievement;
		private Achievement buildingConsumedAchievement;
		private Achievement amountOfPlotsAchievement;

		private List<Achievement> achievements;

		private void Start()
		{
			achievements                = new List<Achievement>();
			
			buildingBuildAchievement    = new Achievement(new int[] {5, 10, 15}, "Build Buildings :");
			rentCollectedAchievement    = new Achievement(new int[] {1000, 10000, 100000, 200000}, "Collect Rent :");
			buildingSavedAchievement    = new Achievement(new int[] {10, 20, 30}, "Save Buildings :");
			buildingConsumedAchievement = new Achievement(new int[] {5, 10, 20}, "Consume Buildings :");
			amountOfPlotsAchievement    = new Achievement(new int[] {5, 10, 20}, "Build Plots :");
			
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
				prefabAchievement.GetComponent<ProgressImageHandler>().Instantiate(achievements[i]);
				achievements[i].PrintAchievement(prefabAchievement);
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
	}
}