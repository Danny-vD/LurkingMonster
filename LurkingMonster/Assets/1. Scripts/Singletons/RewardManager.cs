using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Events;
using Events.Achievements;
using Events.SoilSamplesManagement;
using Gameplay.Achievements;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Singleton;

namespace Singletons
{
	public class RewardManager : Singleton<RewardManager>
	{
		private int counter = 0;
		private AchievementManager achievementManager;

		protected override void Awake()
		{
			if (UserSettings.SettingsExist)
			{
				counter = UserSettings.GameData.AchievementCounter;
			}

			achievementManager      =  FindObjectOfType<AchievementManager>(true);
			UserSettings.OnGameQuit += SaveData;
		}

		private void SaveData()
		{
			UserSettings.GameData.AchievementCounter = counter;
		}
		
		public void Unlock(int amount)
		{
			counter--;
			EventManager.Instance.RaiseEvent(new IncreaseSoilSamplesEvent(amount));
			EventManager.Instance.RaiseEvent(new AchievementUnlockedEvent());
			CheckIfAllAchievementsUnlocked();
		}

		public void IncreaseCounter()
		{
			counter++;
		}

		private void CheckIfAllAchievementsUnlocked()
		{
			if (achievementManager.AllAchievementsUnlocked())
			{
				EventManager.Instance.RaiseEvent(new EndGameEvent());
			}
		}

		public int Counter => counter;
	}
}