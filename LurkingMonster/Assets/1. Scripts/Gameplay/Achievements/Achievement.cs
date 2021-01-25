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

		/// <summary>
		/// Function that checks if an achievement is unlocked
		/// </summary>
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
						
						RewardManager.Instance.IncreaseCounter();
						EventManager.Instance.RaiseEvent(new AchievementUnlockedEvent());
						return;
					}
				}
			}
		}

		/// <summary>
		/// Function that returns achievement data
		/// </summary>
		public AchievementData GetData()
		{
			return new AchievementData(counter, unlocked, rewardsCollected);
		}

		/// <summary>
		/// Function that returns json string
		/// </summary>
		/// <returns>Json string</returns>
		public string GetTitleString()
		{
			return LanguageUtil.GetJsonString(keyMessage);
		}

		/// <summary>
		/// Function that checks if an reward is ready
		/// </summary>
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
		
		/// <summary>
		/// Function that sets the data for this achievement
		/// </summary>
		public void SetData(AchievementData data)
		{
			counter          = data.counter;
			unlocked         = data.unlocked;
			rewardsCollected = data.collected;
		}
		
		/// <summary>
		/// Function that collect the reward for the achievement
		/// </summary>
		public void CollectReward()
		{
			int i = GetIndexFirstNotCollectedReward();
			rewardsCollected[i] = true;
			RewardManager.Instance.Unlock(soilSamples[i]);
		}

		/// <summary>
		/// Function that returns the reward amount
		/// </summary>
		/// <returns>Reward amount</returns>
		public int GetRewardAmount()
		{
			int i = GetIndexFirstNotCollectedReward();
			return soilSamples[i];
		}
		
		/// <summary>
		/// Function that returns an json string
		/// </summary>
		/// <returns>Json string</returns>
		public string GetAchievementInfo()
		{
			return LanguageUtil.GetJsonString(achievementInfo);
		}
		
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
		
		/// <summary>
		/// Function that prints the achievement data to the prefab
		/// </summary>
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