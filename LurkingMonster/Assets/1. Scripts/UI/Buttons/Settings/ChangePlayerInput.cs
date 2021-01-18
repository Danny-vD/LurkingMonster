using Events;
using Events.MoneyManagement;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
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

		[SerializeField]
		private SerializableDictionary<string, int> cheats;
		
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
			
			submit.onClick.RemoveAllListeners();
			submit.onClick.AddListener(SubmitInput);
		}

		private void SubmitInput()
		{
			inputScreen.SetActive(false);

			string username = inputName.text;
			
			UserSettings.GameData.UserName = username;
			UserSettings.GameData.CityName = inputCity.text;

			CheckCheats(username);
			
			EventManager.Instance.RaiseEvent(new InputChangedEvent());
		}

		private void CheckCheats(string cheat)
		{
			if (cheats.ContainsKey(cheat))
			{
				EventManager.Instance.RaiseEvent(new IncreaseMoneyEvent(cheats[cheat]));
			}
		}
	}
}