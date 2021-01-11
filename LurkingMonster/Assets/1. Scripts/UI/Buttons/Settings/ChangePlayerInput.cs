using Events;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Buttons.Settings
{
	public class ChangePlayerInput : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject inputScreen;

		[SerializeField]
		private TMP_InputField inputName;
		
		[SerializeField]
		private TMP_InputField inputCity;
		
		private Button showInputField;
		private Button submit;
		
		private void Start()
		{
			showInputField = GetComponent<Button>();
			
			showInputField.onClick.AddListener(OpenInputScreen);
		}

		private void OpenInputScreen()
		{
			inputScreen.SetActive(true);

			inputName.text = UserSettings.GameData.UserName;
			inputCity.text = UserSettings.GameData.CityName;
			
			submit = inputScreen.GetComponentInChildren<Button>();
			
			submit.onClick.AddListener(SubmitInput);
		}

		private void SubmitInput()
		{
			inputScreen.SetActive(false);
			
			UserSettings.GameData.UserName = inputName.text;
			UserSettings.GameData.CityName = inputCity.text;

			EventManager.Instance.RaiseEvent(new InputChangedEvent());
		}
	}
}