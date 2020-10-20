using System;
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
		[SerializeField]
		private int startMoney = 10000;

		protected override void Awake()
		{
			base.Awake();
			
			gameData = new GameData("", "", startMoney);
			
			destination = Application.persistentDataPath + "/save.dat";

			if (File.Exists(destination))
			{
				ReloadData();
			}
		}

		private void OnApplicationQuit()
		{
			SaveFile();
		}

		public void ReloadData()
		{
			FileStream file;
 
			if(File.Exists(destination)) file = File.OpenRead(destination);
			else
			{
				Debug.LogError("File not found");
				return;
			}
 
			BinaryFormatter bf = new BinaryFormatter();
			gameData                           = (GameData) bf.Deserialize(file);
			file.Close();
		}
		
		public void SaveFile()
		{
			string destination = Application.persistentDataPath + "/save.dat";
			FileStream file;

			if (File.Exists(destination)) file = File.OpenWrite(destination);
			else file                          = File.Create(destination);
			
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, gameData);
			file.Close();
		}

		public GameData GameData
		{
			get => gameData;
		}
	}
}