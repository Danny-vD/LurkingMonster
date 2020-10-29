using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

		protected override void Awake()
		{
			base.Awake();
			destination = Application.persistentDataPath + "/save.dat";

			if (SettingsExist())
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

		public static bool SettingsExist() => File.Exists(destination);
		
		public static void ReloadData()
		{
			FileStream file;

			if (SettingsExist())
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
			
			print(tile.GridPosition + ": " + tile.TileType);
		}

		private void SaveFile()
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
				new Dictionary<Vector2IntSerializable, TileData>());

			RunTimeTests.TestStartMoney();
		}
	}
}