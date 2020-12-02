﻿using UnityEngine;

namespace _1._Scripts.Tutorial
{
	public class NarratorTutorial : Tutorial
	{
		public override void StartTutorial(GameObject narrator)
		{
			base.StartTutorial(narrator);
			ShowNextText();
			SetNextButton(ShowNextText);
		}

		protected override void ReachEndOfText()
		{
			TutorialManager.Instance.CompletedTutorial();
		}
	}
}