using System;

namespace Utility
{
	[Serializable]
	public class GameData
	{
		private string cityName;
		private string userName;
		private int money;

		public GameData(string cityName, string userName, int money)
		{
			this.cityName = cityName;
			this.userName = userName;
			this.money    = money;
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
	}
}