using TMPro;
using UnityEngine;

namespace _1._Scripts.Tutorial
{
	public class InputTutorial : global::Tutorial.Tutorial
	{
		private TMP_InputField inputText;
		
		

		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			base.StartTutorial(narrator, arrow);
			
			arrow.gameObject.SetActive(false);
			narrator.gameObject.SetActive(false);
			
			inputText = GetComponent<TMP_InputField>();
			inputText.gameObject.SetActive(true);
			SetNextButton(CompleteTutorial);
			inputText.onEndEdit.AddListener(SaveText);
		}

		private void SaveText(string input)
		{
			arrow.gameObject.SetActive(true);
		}

		private void CompleteTutorial()
		{
			TutorialManager.Instance.CompletedTutorial();
			inputText.gameObject.SetActive(false);
		}
	}
}