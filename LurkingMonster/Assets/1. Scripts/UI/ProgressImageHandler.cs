using Events.Achievements;
using Gameplay.Achievements;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class ProgressImageHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private Transform parent = null;

		[SerializeField]
		private GameObject prefabImage = null;

		[SerializeField]
		private Color collectedColor = Color.green;
		
		[SerializeField]
		private Color unlockedColor = Color.yellow;

		[SerializeField]
		private Color lockedColor = Color.red;

		[SerializeField]
		private Button collectReward = null;

		private Achievement achievement;
		
		public void Instantiate(Achievement achievement)
		{
			this.achievement = achievement;
			
			for (int i = 0; i < achievement.Unlocked.Length; i++)
			{
				GameObject prefabInstance = Instantiate(prefabImage, parent);

				Image image = prefabInstance.GetComponent<Image>();

				if (achievement.Unlocked[i])
				{
					image.color = achievement.rewardsCollected[i] ? collectedColor : unlockedColor;
				}
				else
				{
					image.color = lockedColor;
				}
			}
			
			collectReward.onClick.AddListener(ShowPopup);
		}

		private void OnDestroy()
		{
			collectReward.onClick.RemoveListener(ShowPopup);
		}

		private void ShowPopup()
		{
			EventManager.Instance.RaiseEvent(new AchievementTappedEvent(achievement));
		}
	}
}
