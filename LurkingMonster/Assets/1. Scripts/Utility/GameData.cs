using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Enums;
using Structs;
using UnityEngine;

namespace Utility
{
	[Serializable]
	public class GameData
	{
		private string cityName;
		private string userName;
		private int money;
		private bool vibrate;
		private float musicVolume;
		private float ambientVolume;
		private Language language;
		private AchievementData[] achievementData;
		private int[] powerUps;
		private PowerUpType powerUpType;
		private float timerPowerUp;

		private Dictionary<Vector2IntSerializable, TileData> dictionary;

		public GameData(
			string                                       cityName,
			string                                       userName,
			int                                          money,
			bool                                         vibrate,
			float                                        musicVolume,
			float                                        ambientVolume,
			Dictionary<Vector2IntSerializable, TileData> dictionary,
			Language                                     language,
			int[]                                        powerUps,
			AchievementData[]                            achievementData,
			PowerUpType powerUpType,
			float timerPowerUp
		)
		{
			this.cityName        = cityName;
			this.userName        = userName;
			this.money           = money;
			this.vibrate         = vibrate;
			this.musicVolume     = musicVolume;
			this.ambientVolume   = ambientVolume;
			this.dictionary      = dictionary;
			this.language        = language;
			this.powerUps        = powerUps;
			this.achievementData = achievementData;
			this.powerUpType     = powerUpType;
			this.timerPowerUp    = timerPowerUp;
		}

		public string CityName
		{
			get => cityName;
			set => cityName = value;
		}

		public string UserName
		{
			get => userName;
			set => userName = value;
		}

		public int Money
		{
			get => money;
			set => money = value;
		}

		public bool Vibrate
		{
			get => vibrate;
			set => vibrate = value;
		}

		public float MusicVolume
		{
			get => musicVolume;
			set => musicVolume = value;
		}

		public float AmbientVolume
		{
			get => ambientVolume;
			set => ambientVolume = value;
		}

		public Dictionary<Vector2IntSerializable, TileData> GridData
		{
			get => dictionary;
			set => dictionary = value;
		}

		public Language Language
		{
			get => language;
			set => language = value;
		}

		public AchievementData[] AchievementData
		{
			get => achievementData;
			set => achievementData = value;
		}

		public int[] PowerUps
		{
			get => powerUps;
			set => powerUps = value;
		}

		public PowerUpType PowerUpType
		{
			get => powerUpType;
			set => powerUpType = value;
		}

		public float TimerPowerUp
		{
			get => timerPowerUp;
			set => timerPowerUp = value;
		}
	}
}