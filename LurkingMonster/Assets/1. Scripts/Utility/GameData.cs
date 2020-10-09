using System;

namespace Utility
{
	[Serializable]
	public class GameData
	{
		private string cityName;
		private string userName;

		public GameData(string cityName, string userName)
		{
			this.cityName = cityName;
			this.userName = userName;
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
	}
}