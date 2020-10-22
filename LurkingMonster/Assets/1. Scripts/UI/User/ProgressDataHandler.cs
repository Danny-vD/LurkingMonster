using System.Data;
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
			GetComponent<Button>().onClick.AddListener(SetData);
		}

		public void SetData()
		{
			// UserSettings.Instance.GameData.CityName = cityName.text;
			// UserSettings.Instance.GameData.UserName = userName.text;
		}
	}
}