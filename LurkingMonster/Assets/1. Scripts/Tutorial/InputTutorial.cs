using System;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace _1._Scripts.Tutorial
{
	public class InputTutorial : global::Tutorial.Tutorial
	{
		private enum InputData
		{
			playerName,
			cityName
		}
		
		[SerializeField]
		private GameObject inputPopup;
		
		[SerializeField]
		private Button submit;

		[SerializeField]
		private TextMeshProUGUI title;
		
		[SerializeField]
		private InputData inputData;
		
	
		private TMP_InputField inputText;

		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			inputPopup.SetActive(true);
			SetHeader();
			inputText = inputPopup.GetComponentInChildren<TMP_InputField>();
			
			submit.onClick.RemoveAllListeners();
			submit.onClick.AddListener(CompleteTutorial);
		}

		private void CompleteTutorial()
		{
			CheckInputData();
			inputPopup.SetActive(false);
			TutorialManager.Instance.CompletedTutorial();
		}

		private void SetHeader()
		{
			switch (inputData)
			{
				case InputData.playerName:
					title.text = LanguageUtil.GetJsonString("INPUT_NAME");
					break;
				case InputData.cityName:
					title.text = LanguageUtil.GetJsonString("INPUT_CITY");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void CheckInputData()
		{
			switch (inputData)
			{
				case InputData.playerName:
					UserSettings.GameData.UserName = inputText.text;
					break;
				case InputData.cityName:
					UserSettings.GameData.CityName = inputText.text;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}