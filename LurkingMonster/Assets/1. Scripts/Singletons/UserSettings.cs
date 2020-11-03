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
			
			SetVolumeOnLoad();
			SetLanguageOnLoad();
		}

		private static void SetLanguageOnLoad()
		{
			LanguageSettings.Language = gameData.Language;
		}

		private static void SetVolumeOnLoad()
		{
			AudioManager.Instance.SetVolume(BusType.Music, gameData.MusicVolume);
			AudioManager.Instance.SetVolume(BusType.Ambient, gameData.AmbientVolume);
		}
		
		private static void SaveDictionary()
		{
			gameData.GridData.Clear();

			AbstractTile[,] grid = GridUtil.Grid;

			for (int y = 0; y < grid.GetLength(0); y++)
			{
				for (int x = 0; x < grid.GetLength(1); x++)
				{
					SaveTile(grid[y, x]);
				}
			}
		}

		private static void SaveTile(AbstractTile tile)
		{
			gameData.GridData.Add(tile.GridPosition, new TileData(tile));
		}

		private static void SaveFile()
		{
			SaveDictionary();
			FileStream file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameData);
			file.Close();
		}

		public void NewGame()
		{
			gameData = new GameData("", "", startMoney, true, 1f, 1f,
				new Dictionary<Vector2IntSerializable, TileData>(), Language.NL, new AchievementData[0]);

			RunTimeTests.TestStartMoney();
		}
	}
}