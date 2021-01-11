using UnityEngine;

namespace Tutorials
{
	public class NarratorTutorial : Tutorial
	{
		public override void StartTutorial(GameObject arrow, TutorialManager manager)
		{
			base.StartTutorial(arrow, manager);
			ShowNextText();
			SetNextButton(ShowNextText);
		}

		protected override void ReachEndOfText()
		{
			manager.CompletedTutorial();
		}
	}
}