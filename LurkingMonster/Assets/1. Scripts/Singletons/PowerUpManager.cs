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
		private int avoidMonsters;
		
		[SerializeField]
		private int fixProblems;
		
		[SerializeField]
		private int avoidWeather;

		private PowerUp[] powerUps;

		private void Start()
		{
			avoidMonsters = 0;
			fixProblems   = 0;
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
			StartNewTimer(powerUp.Timer, (() => DeactivatePowerUp(powerUpType)));
		}
		
		private void AddPowerUp(PowerUpIncreaseEvent powerType)
		{
			switch (powerType.Type)
			{
				case PowerUpType.AvoidMonster:
					++avoidMonsters;
					break;
				case PowerUpType.FixProblems:
					++fixProblems;
					break;
				case PowerUpType.AvoidWeatherEvent:
					++avoidWeather;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(powerType.Type), powerType.Type, null);
			}
		}

		private void DeactivatePowerUp(PowerUpType powerUpType)
		{
			PowerUp powerUp = powerUps.First(item => item.PowerUpType == powerUpType);
			powerUp.IsActive = false;
		}
		
		private void StartNewTimer(float timer, Action action)
		{
			new GameObject().AddComponent<PowerUpTimer>().Initiate(timer, action);
		}

		public bool AvoidMonsterFeedActive => powerUps[0].IsActive;
		public bool FixProblemsActive => powerUps[1].IsActive;

		public bool AvoidWeatherActive => powerUps[2].IsActive;

		
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