using UnityEngine;
using UnityEngine.UI;

namespace Tutorials
{
	public class ButtonTutorial : Tutorial
	{
		[SerializeField]
		private Button continueButton;
		
		public override void StartTutorial(GameObject arrow, TutorialManager manager)
		{
			base.StartTutorial(arrow, manager);

			if (keyCount != 0)
			{
				ShowNextText();
			}
			
			DisableButtons();
			
			SetNextButton(ShowNextText);
			continueButton.onClick.AddListener(CompleteTutorial);
		}
		
		private void DisableButtons()
		{
			Button[] buttons = FindObjectsOfType<Button>();

			foreach (Button disableButton in buttons)
			{
				if (continueButton == disableButton || disableButton == manager.StopTutorialButton)
				{
					continue;
				}

				disableButton.onClick.RemoveAllListeners();
			}
		}

		private void CompleteTutorial()
		{
			manager.CompletedTutorial();
		}
	}
}