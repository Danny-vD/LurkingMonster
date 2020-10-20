using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Utility;
using VDFramework.Singleton;

namespace Singletons
{
	public class UserSettings : Singleton<UserSettings>
	{
		private GameData gameData;
		private string destination;

		protected override void Awake()
		{
			base.Awake();

			destination = Application.persistentDataPath + "/save.dat";

			if (File.Exists(destination))
			{
				ReloadData();
			}
		}

		public void ReloadData()
		{
			FileStream file;

			if (File.Exists(destination)) file = File.OpenRead(destination);
			else
			{
				Debug.LogError("File not found");
				return;
			}

			BinaryFormatter bf = new BinaryFormatter();
			gameData = (GameData) bf.Deserialize(file);
			file.Close();

			print(gameData.CityName);
			print(gameData.UserName);
		}

		public GameData GameData1
		{
			get => gameData;
		}
	}
}