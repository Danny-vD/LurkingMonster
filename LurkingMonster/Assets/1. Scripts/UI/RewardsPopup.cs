using System;
using Events;
using Gameplay.Achievements;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class RewardsPopup : BetterMonoBehaviour
	{
		[SerializeField]
		private Text achievementInfo = null;
		
		[SerializeField]
		private Text rewardInfo = null;

		[SerializeField]
		private Button collectReward = null;
		
		private void Start()
		{
			gameObject.SetActive(false);
			EventManager.Instance.AddListener<AchievementTappedEvent>(SpawnPopup);
		}

		private void SpawnPopup(AchievementTappedEvent achievementTappedEvent)
		{
			gameObject.SetActive(true);
			
			Achievement achievement = achievementTappedEvent.Achievement;
			achievementInfo.text = achievement.GetAchievementInfo();
			rewardInfo.text      = achievement.RewardInfo();
			collectReward.onClick.RemoveAllListeners();
			collectReward.onClick.AddListener(achievement.CollectReward);
			
			collectReward.onClick.AddListener(() => gameObject.SetActive(false));
		}
	}
}