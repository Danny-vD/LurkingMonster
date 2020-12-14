using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Events;
using Events.Achievements;
using Gameplay.Achievements;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class RewardsPopup : BetterMonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI title = null;
		
		[SerializeField]
		private TextMeshProUGUI achievementInfo = null;
		
		[SerializeField]
		private TextMeshProUGUI rewardInfo = null;

		[SerializeField]
		private Button collectReward = null;

		[SerializeField]
		private Image RewardImage = null;

		[SerializeField]
		private SerializableEnumDictionary<SoilType, Sprite> soilSprites;
		
		[SerializeField]
		private SerializableEnumDictionary<FoundationType, Sprite> foundationSprites;
		
		[SerializeField]
		private SerializableEnumDictionary<BuildingType, Sprite> buildingSprites;
		
		[SerializeField]
		private SerializableEnumDictionary<PowerUpType, Sprite> powerUpSprites;
		
		private void Start()
		{
			gameObject.SetActive(false);
			EventManager.Instance.AddListener<AchievementTappedEvent>(SpawnPopup);
		}

		private void SpawnPopup(AchievementTappedEvent achievementTappedEvent)
		{
			gameObject.SetActive(true);
			
			Achievement achievement = achievementTappedEvent.Achievement;

			collectReward.onClick.RemoveAllListeners();
			RewardImage.sprite   = CheckType(achievement.GetNextReward());
			title.text           = achievement.GetTitleString();
			achievementInfo.text = achievement.GetAchievementInfo();
			rewardInfo.text      = achievement.RewardInfo();
			
			if (!achievement.CheckIfRewardReady())
			{
				collectReward.interactable = false;
				return;
			}

			collectReward.interactable = true;
			collectReward.onClick.AddListener(achievement.CollectReward);
			
			collectReward.onClick.AddListener(() => gameObject.SetActive(false));
		}

		private Sprite CheckType(object obj)
		{
			switch (obj)
			{
				case SoilType soilType:
					return GetSprite<SoilType>(soilSprites, obj);
				case FoundationType foundationType:
					return GetSprite<FoundationType>(foundationSprites, obj);
				case BuildingType buildingType:
					return GetSprite<BuildingType>(buildingSprites, obj);
				case PowerUpType powerUpType:
					return GetSprite<PowerUpType>(powerUpSprites, obj);
				default:
					return null;
			}
		}

		private Sprite GetSprite<TKey>(Dictionary<TKey, Sprite> dictionary, object obj)
		{
			return dictionary.TryGetValue((TKey) obj, out Sprite sprite) ? sprite : null;
		}
	}
}