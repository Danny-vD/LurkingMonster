using System;
using Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Achievements
{
	public class Achievement
	{
		private readonly int[] limits;
		private readonly bool[] unlocked;
		private readonly string message;
		private int counter;
		
		public Achievement(int[] limits, string message)
		{
			this.limits      = limits;
			this.message     = message;
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
						MessageManager.Instance.ShowMessageGameUI("Achievement Unlocked", Color.green);

						//TODO show achievement!!
						return;
					}
				}
			}
		}

		public void PrintAchievement(GameObject prefabAchievement)
		{
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
			prefabAchievement.GetComponentsInChildren<Text>()[1].text = $"All Achievements Unlocked";
				
			prefabAchievement.GetComponentInChildren<Image>().color = Color.green;
		}
	}
}