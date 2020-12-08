using UnityEngine;
using UnityEngine.UI;

namespace Tutorials
{
	public class HighlightTutorial : Tutorial
	{
		[SerializeField]
		private GameObject highlight;

		[SerializeField]
		private Button button;

		[SerializeField]
		private float scale;

		[SerializeField]
		private float offsetDistance = 250f;
		
		private GameObject prefabInstance;

		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			base.StartTutorial(narrator, arrow);
			ShowNextText();
			
			DisableButtons();
			
			SetNextButton(Highlight);
			button.onClick.AddListener(CompleteTutorial);
		}

		private void DisableButtons()
		{
			Button[] buttons = FindObjectsOfType<Button>();

			foreach (Button disableButton in buttons)
			{
				if (button == disableButton)
				{
					continue;
				}
				
				disableButton.onClick.RemoveAllListeners();
			}
		}
		
		private void Highlight()
		{
			ShowNextText();
			prefabInstance = Instantiate(arrow, highlight.transform, true);

			prefabInstance.transform.localPosition = Vector3.zero;
			prefabInstance.transform.localScale = new Vector3(scale, scale, scale);
			prefabInstance.transform.Translate(Vector3.up * offsetDistance);
		}

		private void CompleteTutorial()
		{
			Destroy(prefabInstance);
			TutorialManager.Instance.CompletedTutorial();
		}
	}
}