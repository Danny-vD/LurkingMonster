using System;
using System.Collections.Generic;
using Enums;
using Structs;

namespace Utility
{
	[Serializable]
	public class GameData
	{
		//TODO: split up in serializable structs

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
		private WeatherEventType weatherEventType;
		private float timerWeatherEvent;

		private Dictionary<Vector2IntSerializable, TileData> dictionary;

		// TODO: most of these parameters are unecessary I think, we only call the Ctor when we create a new game
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
			PowerUpType                                  powerUpType,
			float                                        timerPowerUp,
			WeatherEventType                             weatherEventType,
			float                                        timerWeatherEvent
		)
		{
			this.cityName          = cityName;
			this.userName          = userName;
			this.money             = money;
			this.vibrate           = vibrate;
			this.musicVolume       = musicVolume;
			this.ambientVolume     = ambientVolume;
			this.dictionary        = dictionary;
			this.language          = language;
			this.powerUps          = powerUps;
			this.achievementData   = achievementData;
			this.powerUpType       = powerUpType;
			this.timerPowerUp      = timerPowerUp;
			this.weatherEventType  = weatherEventType;
			this.timerWeatherEvent = timerWeatherEvent;
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

		public WeatherEventType WeatherEventType
		{
			get => weatherEventType;
			set => weatherEventType = value;
		}

		public float TimerWeatherEvent
		{
			get => timerWeatherEvent;
			set => timerWeatherEvent = value;
		}
	}
}