using UnityEngine;

namespace Tutorials
{
	public class NarratorTutorial : Tutorial
	{
		public override void StartTutorial(GameObject arrow)
		{
			base.StartTutorial(arrow);
			ShowNextText();
			SetNextButton(ShowNextText);
		}

		protected override void ReachEndOfText()
		{
			TutorialManager.Instance.CompletedTutorial();
		}
	}
}