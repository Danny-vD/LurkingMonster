using System;
using Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Achievements
{
	public class Achievement
	{
		private readonly int[] experiences;
		private readonly int[] limits;
		private readonly bool[] unlocked;
		private readonly string message;
		
		public static LevelBar LevelBar;

		public Achievement(int[] experiences, int[] limits, string message)
		{
			this.experiences = experiences;
			this.limits      = limits;
			this.message     = message;

			unlocked = new bool[experiences.Length];
		}

		public void CheckAchievement(int counter)
		{
			for (int i = 0; i < unlocked.Length; i++)
			{
				if (!unlocked[i])
				{
					if (counter >= limits[i])
					{
						unlocked[i] = true;
						LevelBar.AddExperience(experiences[i]);
						//For now show message
						MassageManager.Instance.ShowMessageGameUI(limits[i] + " " + message, Color.green);
						//TODO show achievement!!
						return;
					}
				}
			}
		}

		public void PrintAchievementProgress(GameObject prefabAchievment, int houseCounter)
		{
			prefabAchievment.GetComponentInChildren<Text>().text = $"{houseCounter} {message}";
		}
	}
}