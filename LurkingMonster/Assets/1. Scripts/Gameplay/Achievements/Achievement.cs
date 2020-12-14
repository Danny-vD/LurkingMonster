using System;
using System.Collections.Generic;
using System.Data;
using Enums;
using Events;
using Events.Achievements;
using IO;
using Singletons;
using Structs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework.EventSystem;
using Object = System.Object;

namespace Gameplay.Achievements
{
	public class Achievement
	{
		private readonly int[] limits;
		private bool[] unlocked;
		private readonly string keyMessage;
		private int counter;

		private readonly string achievementInfo;

		private readonly object[] objects;

		public bool[] rewardsCollected { get; private set;}
		
		public Achievement(int[] limits, string keyMessage, object[] objects, string achievementInfo)
		{
			this.limits          = limits;
			this.keyMessage      = keyMessage;
			counter              = 0;
			this.objects         = objects;
			this.achievementInfo = achievementInfo;
			
			unlocked             = new bool[limits.Length];
			rewardsCollected     = new bool[objects.Length];
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
						
						//TODO show achievement!!
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
			return unlocked[counter];
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
			RewardManager.Instance.Unlock(objects[i]);
		}

		public object GetNextReward()
		{
			return objects[GetIndexFirstNotCollectedReward()];
		}
		
		public string GetAchievementInfo()
		{
			return LanguageUtil.GetJsonString(achievementInfo);
		}

		public string RewardInfo()
		{
			return LanguageUtil.GetJsonString(LanguageUtil.GetRewardInfo(objects[GetIndexFirstNotCollectedReward()]));
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