using System;
using System.Linq;
using Enums;
using Events;
using Gameplay;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;

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
			avoidMonsters = 1;
			fixProblems   = 3;
			avoidWeather  = 0;

			powerUps = new[]
			{
				new PowerUp(false, 60f, "Monster Feed", PowerUpType.AvoidMonster),
				new PowerUp(false, 10f, "KCAF Manager", PowerUpType.FixProblems),
				new PowerUp(false, 10f, "Time Stop", PowerUpType.AvoidWeatherEvent)
			};

			EventManager.Instance.AddListener<PowerUpIncreaseEvent>(AddPowerUp);
		}

		public void ActivatePowerUp(PowerUpType powerUpType)
		{
			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = true;
			powerUpTimer.StartTimer(powerUp.Timer, () => DeactivatePowerUp(powerUpType), powerUpType);
			
			ChangePowerUpAmount(powerUpType, -1);
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