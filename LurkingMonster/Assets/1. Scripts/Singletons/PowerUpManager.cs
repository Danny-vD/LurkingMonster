﻿using System;
using System.Linq;
using Enums;
using Events;
using Gameplay;
using IO;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;
using VDFramework.Extensions;

namespace Singletons
{
	public class PowerUpManager : Singleton<PowerUpManager>
	{
		[SerializeField]
		private PowerUpTimer powerUpTimer;
		
		[SerializeField]
		private int avoidMonsters;
		
		[SerializeField]
		private int fixProblems;
		
		[SerializeField]
		private int avoidWeather;

		private PowerUp[] powerUps;

		private bool powerUpActive;

		protected override void Awake()
		{
			base.Awake();
			
			powerUps = new[]
			{
				//TODO: use a formatted json string to print the actual time for the description
				new PowerUp(false, 120f, "Monster Feed", PowerUpType.AvoidMonster, 2000),
				new PowerUp(false, 120f, "KCAF Manager", PowerUpType.FixProblems, 10000),
				new PowerUp(false, 300f, "Time Stop", PowerUpType.AvoidWeatherEvent, 5000),
			};

			if (UserSettings.SettingsExist)
			{
				avoidMonsters = UserSettings.GameData.PowerUps[0];
				avoidWeather  = UserSettings.GameData.PowerUps[1];
				fixProblems   = UserSettings.GameData.PowerUps[2];
				
				ActivatePowerUpOnLoad(UserSettings.GameData.TimerPowerUp, UserSettings.GameData.PowerUpType);
			}

			UserSettings.OnGameQuit += SavePowerUps;
		}

		public void ActivatePowerUp(PowerUpType powerUpType)
		{
			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = true;
			powerUpTimer.StartTimer(powerUp.Timer, () => DeactivatePowerUp(powerUpType), powerUpType);
			
			ChangePowerUpAmount(powerUpType, -1);
			
			EventManager.Instance.RaiseEvent(new PowerUpActivateEvent(powerUpType));
		}

		private void ActivatePowerUpOnLoad(float time, PowerUpType powerUpType)
		{
			if (powerUpType == (PowerUpType) (-1))
			{
				return;
			}

			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = true;
			powerUpTimer.StartTimer(powerUp.Timer, () => DeactivatePowerUp(powerUpType), powerUpType);
			powerUpTimer.Timer = time;
			
			EventManager.Instance.RaiseEvent(new PowerUpActivateEvent(powerUpType));
		}

		private void DeactivatePowerUp(PowerUpType powerUpType)
		{
			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = false;
			
			EventManager.Instance.RaiseEvent(new PowerUpDisableEvent(powerUpType));
		}
		
		private void ChangePowerUpAmount(PowerUpType powerType, int amount)
		{
			switch (powerType)
			{
				case PowerUpType.AvoidMonster:
					avoidMonsters += amount;
					break;
				case PowerUpType.FixProblems:
					fixProblems += amount;
					break;
				case PowerUpType.AvoidWeatherEvent:
					avoidWeather += amount;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(powerType), powerType, null);
			}
		}

		private int ReturnActivePowerUpIndex(out float timer)
		{
			foreach (PowerUp powerUp in powerUps)
			{
				if (!powerUp.IsActive)
				{
					continue;
				}

				timer = powerUpTimer.Timer;
				return (int) powerUp.PowerUpType;
			}

			timer = 0;
			return -1;
		}
		
		private void SavePowerUps()
		{
			GameData gameData = UserSettings.GameData;
			gameData.PowerUps    = new int[powerUps.Length];
			gameData.PowerUps[0] = AvoidMonsters;
			gameData.PowerUps[1] = AvoidWeather;
			gameData.PowerUps[2] = FixProblems;

			if (!CheckIfAnPowerUpIsActive())
			{
				gameData.PowerUpType = (PowerUpType) (-1);
				return;
			}
		
			int index = ReturnActivePowerUpIndex(out float timer);
			gameData.PowerUpType  = default(PowerUpType).GetValues().ElementAt(index);
			gameData.TimerPowerUp = timer;
		}

		public bool AvoidMonsterFeedActive => powerUps[0].IsActive;
		public bool FixProblemsActive => powerUps[1].IsActive;
		public bool AvoidWeatherActive => powerUps[2].IsActive;

		public bool CheckIfAnPowerUpIsActive()
		{
			return powerUps.Any(powerUp => powerUp.IsActive);
		}

		public PowerUp GetPowerUp(PowerUpType powerUpType)
		{
			return powerUps.First(item => item.PowerUpType == powerUpType);
		}
		
		public int AvoidMonsters
		{
			get => avoidMonsters;
			set
			{
				avoidMonsters = value;
				EventManager.Instance.RaiseEvent(new PowerUpIncreaseEvent());
			}
		}

		public int FixProblems
		{
			get => fixProblems;
			set
			{
				fixProblems = value;
				EventManager.Instance.RaiseEvent(new PowerUpIncreaseEvent());
			}
		}

		public int AvoidWeather
		{
			get => avoidWeather;
			set
			{
				avoidWeather = value;
				EventManager.Instance.RaiseEvent(new PowerUpIncreaseEvent());
			}
		}
	}
}