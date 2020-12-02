using UnityEngine;

namespace _1._Scripts.Tutorial
{
	public class HighlightTutorial : Tutorial
	{
		[SerializeField]
		private GameObject[] highlights;

		private int index;
		
		public override void StartTutorial(GameObject narrator)
		{
			base.StartTutorial(narrator);
			index = 0;
			ShowNextText();
			Highlight(highlights[index]);
			SetNextButton(NextHighlight);
		}

		private void NextHighlight()
		{
			ShowNextText(); 
			
			UnHighlight(highlights[index]);
			index++;
			
			if (index >= highlights.Length)
			{
				TutorialManager.Instance.CompletedTutorial();
				return;
			}
			
			Highlight(highlights[index]);
		}

		private void Highlight(GameObject gameObject)
		{
		}

		private void UnHighlight(GameObject gameObject)
		{
		}
	}
}