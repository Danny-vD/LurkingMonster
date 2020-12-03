using UnityEngine;

namespace _1._Scripts.Tutorial
{
	public class NarratorTutorial : Tutorial
	{
		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			base.StartTutorial(narrator, arrow);
			ShowNextText();
			SetNextButton(ShowNextText);
		}

		protected override void ReachEndOfText()
		{
			TutorialManager.Instance.CompletedTutorial();
		}
	}
}