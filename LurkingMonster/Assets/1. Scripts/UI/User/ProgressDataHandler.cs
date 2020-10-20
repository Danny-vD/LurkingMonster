using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace UI.User
{
	public class ProgressDataHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private InputField cityName;
		
		[SerializeField]
		private InputField userName;

		public void Start()
		{
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SaveFile);
		}

		public void SaveFile()
		{
			string destination = Application.persistentDataPath + "/save.dat";
			FileStream file;

			if (File.Exists(destination)) file = File.OpenWrite(destination);
			else file                          = File.Create(destination);
			
			GameData data = new GameData(cityName.text, userName.text);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, data);
			file.Close();
		}
	}
}