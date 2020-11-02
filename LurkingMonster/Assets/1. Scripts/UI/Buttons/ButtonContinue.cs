using System.IO;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class ButtonContinue : BetterMonoBehaviour
	{
		private void Start()
		{
			DisableContinueButton();
		}

		public void DisableContinueButton()
		{
			Button buttonContinue = GetComponent<Button>();
			string destination = Application.persistentDataPath + "/save.dat";

			if (File.Exists(destination))
			{
				return;
			}

			buttonContinue.interactable = false;
		}
	}
}