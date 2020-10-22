using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Utility;
using VDFramework.Singleton;

namespace Singletons
{
	public class UserSettings : Singleton<UserSettings>
	{
		private static GameData gameData;
		private string destination;

		[SerializeField]
		private int startMoney = 10000;

		public static GameData GameData
		{
			get
			{
				if (gameData == null)
				{
					// Force it to initialise
					_ = Instance;
				}

				return gameData;
			}
		}

		protected override void Awake()
		{
			base.Awake();

			gameData = new GameData("", "", startMoney, true, 1f, 1f);

			destination = Application.persistentDataPath + "/save.dat";

			if (File.Exists(destination))
			{
				ReloadData();
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				SaveFile();
			}
		}
		
		private void OnApplicationQuit()
		{
			SaveFile();
		}

		public void ReloadData()
		{
			FileStream file;

			if (File.Exists(destination))
			{
				file = File.OpenRead(destination);
			}
			else
			{
				Debug.LogError("File not found");
				return;
			}

			BinaryFormatter bf = new BinaryFormatter();
			gameData = (GameData) bf.Deserialize(file);
			file.Close();
		}

		public void SaveFile()
		{
			FileStream file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameData);
			file.Close();
		}
	}
}