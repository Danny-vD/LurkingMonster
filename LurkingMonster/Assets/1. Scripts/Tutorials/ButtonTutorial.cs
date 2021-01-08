using UnityEngine;
using UnityEngine.UI;

namespace Tutorials
{
	public class ButtonTutorial : Tutorial
	{
		[SerializeField]
		private Button continueButton;
		
		public override void StartTutorial(GameObject arrow)
		{
			base.StartTutorial(arrow);

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
				if (continueButton == disableButton || disableButton == TutorialManager.Instance.StopTutorialButton)
				{
					continue;
				}

				disableButton.onClick.RemoveAllListeners();
			}
		}

		private static void CompleteTutorial()
		{
			TutorialManager.Instance.CompletedTutorial();
		}
	}
}