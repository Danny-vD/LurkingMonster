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
			achievementInfo.text = achievement.GetAchievementInfo();
			title.text           = achievement.GetTitleString();
			rewardInfo.text      = achievement.GetRewardAmount().ToString();
			
			if (!achievement.CheckIfRewardReady())
			{
				collectReward.interactable = false;
				return;
			}

			collectReward.interactable = true;
			collectReward.onClick.AddListener(OnClick);

			void OnClick()
			{
				achievement.CollectReward();
				CachedTransform.GetChild(0).gameObject.SetActive(false);
				EventManager.Instance.RaiseEvent(new RewardCollectedEvent());
			}
		}
	}
}