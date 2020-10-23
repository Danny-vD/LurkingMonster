using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Grid;
using Grid.Tiles;
using Structs;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework.Singleton;

namespace Singletons
{
	using Grid;
	using Grid.Tiles;
	using Grid.Tiles.Building;
	using Grid.Tiles.Road;

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

			gameData = new GameData("", "", startMoney, true, 1f, 1f, new Dictionary<Vector2Int, TileData>());

			destination = Application.persistentDataPath + "/save.dat";

			if (!File.Exists(destination))
			{
				return;
			}
			
			ReloadData();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				SaveFile();
			}
		}

		private void SaveDictionary()
		{
			AbstractTile[,] grid = GridUtil.Grid;

			for (int y = 0; y < GridUtil.GridData.GridSize.y; y++)
			{
				for (int x = 0; x < GridUtil.GridData.GridSize.x; x++)
				{
					AbstractTile tile = grid[y, x];
					SaveTile(tile);
				}
			}
		}

		private void SaveTile(AbstractTile tile)
		{
			Vector2Int gridPosition = tile.GridPosition;
			
			// Using a switch to check and cast to approriate type directly
			switch (tile)
			{
				case AbstractBuildingTile buildingTile:
					// You can get all your information through this one
					break;
				case AbstractRoadTile roadTile:
					// Could be removed, maybe useful later, probably not
					break;
				default:
					break;
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