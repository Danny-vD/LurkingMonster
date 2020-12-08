using UnityEngine;

namespace Tutorials
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