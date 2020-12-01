using System;
using System.Collections.Generic;
using VDFramework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VDFramework.Singleton;

namespace _1._Scripts.Tutorial
{
	public class TutorialManager : Singleton<TutorialManager>
	{
		[SerializeField]
		private GameObject narrator;

		private Tutorial[] tutorials;
		
		private Tutorial currentTutorial;

		private void Start()
		{
			tutorials = GetComponents<Tutorial>();
			SetNextTutorial(0);
		}
		
		public void CompletedTutorial()
		{
			SetNextTutorial(currentTutorial.Order + 1);
		}

		private void SetNextTutorial(int currentOrder)
		{
			currentTutorial = GetTutorialByOrder(currentOrder);

			if (!currentTutorial)
			{
				CompletedAllTutorials();
				return;
			}

			currentTutorial.StartTutorial(narrator);
			//explainText.text = currentTutorial.Explanation;
		}

		private void CompletedAllTutorials()
		{
			//explainText.text = "You completed all the tutorials!";
		}

		private Tutorial GetTutorialByOrder(int order)
		{
			for (int i = 0; i < tutorials.Length; i++)
			{
				if (tutorials[i].Order == order)
				{
					return tutorials[i];
				}
			}

			return null;
		}
	}
}