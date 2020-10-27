using System;
using Enums;
using IO;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Gameplay.Achievements
{
	public class Achievement
	{
		private readonly int[] limits;
		private readonly bool[] unlocked;
		private readonly string keyMessage;
		private int counter;
		
		public Achievement(int[] limits, string keyMessage)
		{
			this.limits      = limits;
			this.keyMessage     = keyMessage;
			counter          = 0;

			unlocked = new bool[limits.Length];
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
						MessageManager.Instance.ShowMessageGameUI(LanguageUtil.GetJsonString("ACHIEVEMENT_UNLOCKED"), Color.green);

						//TODO show achievement!!
						return;
					}
				}
			}
		}

		public void PrintAchievement(GameObject prefabAchievement)
		{
			string message = LanguageUtil.GetJsonString(keyMessage);
			for (int i = 0; i < limits.Length; i++)
			{
				if (!unlocked[i])
				{
					prefabAchievement.GetComponentsInChildren<Text>()[0].text = $"{message} {limits[i]}";
					prefabAchievement.GetComponentsInChildren<Text>()[1].text = $"{counter} / {limits[i]}";
					
					prefabAchievement.GetComponentsInChildren<Bar>()[0].SetMax(limits[i]);
					prefabAchievement.GetComponentsInChildren<Bar>()[0].SetValue(counter);
					return;
				}
			}
			
			prefabAchievement.GetComponentsInChildren<Text>()[0].text = $"{message}";
			prefabAchievement.GetComponentsInChildren<Text>()[1].text = LanguageUtil.GetJsonString("ALL_ACHIEVEMENTS_UNLOCKED");
				
			prefabAchievement.GetComponentInChildren<Image>().color = Color.green;
		}

		public bool[] Unlocked => unlocked;
	}
}