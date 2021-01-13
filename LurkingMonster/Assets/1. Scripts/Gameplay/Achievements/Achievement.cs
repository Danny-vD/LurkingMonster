using System;
using Events.Achievements;
using Singletons;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework.EventSystem;

namespace Gameplay.Achievements
{
	public class Achievement
	{
		private readonly int[] limits;
		private bool[] unlocked;
		private readonly string keyMessage;
		private int counter;

		private readonly string achievementInfo;

		private readonly int[] soilSamples;

		public bool[] rewardsCollected { get; private set;}
		
		public Achievement(int[] limits, string keyMessage, int[] soilSamples, string achievementInfo)
		{
			this.limits          = limits;
			this.keyMessage      = keyMessage;
			counter              = 0;
			this.soilSamples     = soilSamples;
			this.achievementInfo = achievementInfo;
			
			unlocked             = new bool[limits.Length];
			rewardsCollected     = new bool[soilSamples.Length];
		}

		public void CheckAchievement(int value)
		{
			counter += value;
			
			for (int i = 0; i < unlocked.Length; i++)
			{
				if (!unlocked[i])
				{
					if (counter >= limits[i])
					{
						unlocked[i] = true;
						
						//For now show message
						//MessageManager.Instance.ShowMessageGameUI(LanguageUtil.GetJsonString("ACHIEVEMENT_UNLOCKED"), Color.green);
						RewardManager.Instance.IncreaseCounter();
						EventManager.Instance.RaiseEvent(new AchievementUnlockedEvent());
						return;
					}
				}
			}
		}

		public AchievementData GetData()
		{
			return new AchievementData(counter, unlocked, rewardsCollected);
		}

		public string GetTitleString()
		{
			return LanguageUtil.GetJsonString(keyMessage);
		}

		public bool CheckIfRewardReady()
		{
			for (int i = 0; i < unlocked.Length; i++)
			{
				if (unlocked[i] && !rewardsCollected[i])
				{
					return true;
				}
			}

			return false;
		}

		public void SetData(AchievementData data)
		{
			counter          = data.counter;
			unlocked         = data.unlocked;
			rewardsCollected = data.collected;
		}
		
		public void CollectReward()
		{
			int i = GetIndexFirstNotCollectedReward();
			rewardsCollected[i] = true;
			RewardManager.Instance.Unlock(soilSamples[i]);
		}

		/*public string GetAchievementInfo()
		{
			return LanguageUtil.GetJsonString(achievementInfo);
		}

		public string RewardInfo()
		{
			return LanguageUtil.GetJsonString(LanguageUtil.GetRewardInfo(soilSamples[GetIndexFirstNotCollectedReward()]));
		}*/

		private int GetIndexFirstNotCollectedReward()
		{
			for (int i = 0; i < rewardsCollected.Length; i++)
			{
				if (!rewardsCollected[i])
				{
					return i;
				}
			}
			
			throw new Exception("All rewards already collected");
		}
		
		

		public void PrintAchievement(GameObject prefabAchievement)
		{
			string message = LanguageUtil.GetJsonString(keyMessage);
			
			for (int i = 0; i < limits.Length; i++)
			{
				if (!unlocked[i])
				{
					prefabAchievement.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{message} {limits[i]}";
					prefabAchievement.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"{counter} / {limits[i]}";
					
					prefabAchievement.GetComponentsInChildren<Bar>()[0].SetMax(limits[i]);
					prefabAchievement.GetComponentsInChildren<Bar>()[0].SetValue(counter);
					return;
				}
			}
			
			prefabAchievement.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{message}";
			prefabAchievement.GetComponentsInChildren<TextMeshProUGUI>()[1].text = LanguageUtil.GetJsonString("ALL_ACHIEVEMENTS_UNLOCKED");
				
			prefabAchievement.GetComponentInChildren<Image>().color = Color.green;
		}

		public bool[] Unlocked => unlocked;
	}
}