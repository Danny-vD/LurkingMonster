using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VDFramework;

namespace UI.Popups
{
	public class ConfirmPopup : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject popup;

		[SerializeField]
		private Button confirmButton;

		public void ShowPopUp(UnityAction onClick)
		{
			Show();
			onClick += Hide;
			confirmButton.onClick.AddListener(onClick);
		}

		private void Show()
		{
			popup.SetActive(true);
		}
		
		private void Hide()
		{
			popup.SetActive(false);
		}

		private void OnDisable()
		{
			confirmButton.onClick.RemoveAllListeners();
		}
	}
}