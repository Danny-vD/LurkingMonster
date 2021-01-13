using Events.Achievements;
using Gameplay.Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
		
		private void Start()
		{
			EventManager.Instance.AddListener<AchievementTappedEvent>(SpawnPopup);
		}

		private void SpawnPopup(AchievementTappedEvent achievementTappedEvent)
		{
			CachedTransform.GetChild(0).gameObject.SetActive(true);
			
			Achievement achievement = achievementTappedEvent.Achievement;

			collectReward.onClick.RemoveAllListeners();
			title.text           = achievement.GetTitleString();
			/*achievementInfo.text = achievement.GetAchievementInfo();
			rewardInfo.text      = achievement.RewardInfo();*/
			
			if (!achievement.CheckIfRewardReady())
			{
				collectReward.interactable = false;
				return;
			}

			collectReward.interactable = true;
			collectReward.onClick.AddListener(achievement.CollectReward);
			
			collectReward.onClick.AddListener(() => CachedTransform.GetChild(0).gameObject.SetActive(false));
		}
	}
}