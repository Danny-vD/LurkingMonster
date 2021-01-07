using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Enums;
using IO;
using UnityEngine;
using VDFramework.Singleton;

namespace Singletons
{
	public class UserSettings : Singleton<UserSettings>
	{
		private static GameData gameData;
		private static string destination;

		[SerializeField]
		private int startMoney = 10000;

		public delegate void GameQuit();

		public static event GameQuit OnGameQuit; // Create an event that will be called as the application quits

		public static GameData GameData
		{
			get
			{
				if (gameData == null)
				{
					// Force it to initialise
					_ = Instance;
				}

				if (gameData == null)
				{
					Instance.NewGame();
				}

				return gameData;
			}
		}

		public static bool SettingsExist => File.Exists(SavePath);

		public static string SavePath
		{
			get
			{
				if (string.IsNullOrEmpty(destination))
				{
					// Lazy definition
					destination = Application.persistentDataPath + "/save.dat";
				}

				return destination;
			}
		}

		protected override void Awake()
		{
			base.Awake();

			if (SettingsExist)
			{
				ReloadData();
				SetLanguageOnLoad();
			}
		}

		private void OnApplicationQuit()
		{
			OnGameQuit?.Invoke();

			if (gameData == null)
			{
				return;
			}

			SaveFile();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				OnGameQuit?.Invoke();
				
				if (gameData == null)
				{
					return;
				}

				SaveFile();
			}
		}

		private static void ReloadData()
		{
			FileStream file;

			if (SettingsExist)
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

		private static void SetLanguageOnLoad()
		{
			LanguageSettings.Language = gameData.Language;
		}

		private static void SaveFile()
		{
			gameData.Language = LanguageSettings.Language;
			
			FileStream file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameData);
			file.Close();
		}

		public void NewGame()
		{
			bool vibrate = gameData == null || gameData.Vibrate;
 
			gameData = new GameData(startMoney, vibrate);
		}
	}
}