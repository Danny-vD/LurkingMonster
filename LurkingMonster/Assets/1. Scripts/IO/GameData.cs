using System;
using System.Collections.Generic;
using Audio;
using Enums;
using Enums.Audio;
using Structs;

namespace IO
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
		private float sfxVolume;
		private bool masterMute;
		private Language language;
		private AchievementData[] achievementData;
		private int[] powerUps;
		private PowerUpType powerUpType;
		private float timerPowerUp;
		private WeatherEventType weatherEventType;
		private float timerWeatherEvent;
		private int achievementCounter;
		
		private Dictionary<Vector2IntSerializable, TileData> dictionary;
		
		public GameData(int money, bool vibrate)
		{
			this.money         = money;
			dictionary         = new Dictionary<Vector2IntSerializable, TileData>();
			achievementData    = new AchievementData[0];
			powerUps           = new int[2];
			musicVolume        = AudioManager.Instance.GetVolume(BusType.Music);
			ambientVolume      = AudioManager.Instance.GetVolume(BusType.Ambient);
			sfxVolume          = AudioManager.Instance.GetVolume(BusType.SFX);
			masterMute         = AudioManager.Instance.GetVolume(BusType.Master) == 0;
			language           = LanguageSettings.Language;
			this.vibrate       = vibrate;
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

		public float SFXVolume
		{
			get => sfxVolume;
			set => sfxVolume = value;
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

		public bool MasterMute
		{
			get => masterMute;
			set => masterMute = value;
		}

		public int AchievementCounter
		{
			get => achievementCounter;
			set => achievementCounter = value;
		}
	}
}