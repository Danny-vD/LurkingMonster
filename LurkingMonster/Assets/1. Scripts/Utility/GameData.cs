using System;

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

		public GameData(string cityName, string userName, int money, bool vibrate, float musicVolume, float ambientVolume)
		{
			this.cityName      = cityName;
			this.userName      = userName;
			this.money         = money;
			this.vibrate       = vibrate;
			this.musicVolume   = musicVolume;
			this.ambientVolume = ambientVolume;
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
	}
}