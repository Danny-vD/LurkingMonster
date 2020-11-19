using System;
using System.Linq;
using Enums;
using Events;
using Gameplay;
using UnityEngine;
using Utility;
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

		private void Start()
		{
			powerUps = new[]
			{
				new PowerUp(false, 300f, "Monster Feed", PowerUpType.AvoidMonster),
				new PowerUp(false, 1200f, "KCAF Manager", PowerUpType.FixProblems),
				new PowerUp(false, 600f, "Time Stop", PowerUpType.AvoidWeatherEvent)
			};
			
			//TODO change
			if (!UserSettings.SettingsExist)
			{
				avoidMonsters = 1;
				fixProblems   = 3;
				avoidWeather  = 1;
			}
			else
			{
				avoidMonsters = UserSettings.GameData.PowerUps[0];
				avoidWeather  = UserSettings.GameData.PowerUps[1];
				fixProblems   = UserSettings.GameData.PowerUps[2];
				
				ActivatePowerUpOnLoad(UserSettings.GameData.TimerPowerUp, UserSettings.GameData.PowerUpType);
			}

			EventManager.Instance.AddListener<PowerUpIncreaseEvent>(AddPowerUp);
			UserSettings.OnGameQuit += SavePowerUps;
		}

		public void ActivatePowerUp(PowerUpType powerUpType)
		{
			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = true;
			powerUpTimer.StartTimer(powerUp.Timer, () => DeactivatePowerUp(powerUpType), powerUpType);
			
			ChangePowerUpAmount(powerUpType, -1);
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
		}

		private void DeactivatePowerUp(PowerUpType powerUpType)
		{
			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = false;
		}

		private void AddPowerUp(PowerUpIncreaseEvent powerType)
		{
			ChangePowerUpAmount(powerType.Type, 1);
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

		protected override void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<PowerUpIncreaseEvent>(AddPowerUp);
		}

		public bool AvoidMonsterFeedActive => powerUps[0].IsActive;
		public bool FixProblemsActive => powerUps[1].IsActive;

		public bool AvoidWeatherActive => powerUps[2].IsActive;

		public bool CheckIfAnPowerUpIsActive()
		{
			return powerUps.Any(powerUp => powerUp.IsActive);
		}
		
		public int AvoidMonsters
		{
			get => avoidMonsters;
			set => avoidMonsters = value;
		}

		public int FixProblems
		{
			get => fixProblems;
			set => fixProblems = value;
		}

		public int AvoidWeather
		{
			get => avoidWeather;
			set => avoidWeather = value;
		}
	}
}