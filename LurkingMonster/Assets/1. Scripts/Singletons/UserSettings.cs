﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Audio;
using Enums;
using Enums.Audio;
using Grid;
using Grid.Tiles;
using Structs;
using Tests;
using UnityEngine;
using Utility;
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

		public static bool SettingsExist
		{
			get
			{
				if (string.IsNullOrEmpty(destination))
				{
					// Lazy definition
					destination = Application.persistentDataPath + "/save.dat";
				}

				return File.Exists(destination);
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
				if (gameData == null)
				{
					return;
				}

				OnGameQuit?.Invoke();

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
			gameData = new GameData("", "", startMoney, true, 1f, 1f,
				new Dictionary<Vector2IntSerializable, TileData>(), Language.NL, new int[3], new AchievementData[0], default, 0, default,
				0);

			RunTimeTests.TestStartMoney();
		}
	}
}