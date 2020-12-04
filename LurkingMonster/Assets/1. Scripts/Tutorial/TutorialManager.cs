using System;
using System.Collections.Generic;
using Singletons;
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

		[SerializeField]
		private GameObject arrow;
		
		private Tutorial[] tutorials;
		
		private Tutorial currentTutorial;

		public bool IsActive { get; private set; }

		private void Start()
		{
			IsActive  = true;
			tutorials = GetComponents<Tutorial>();
			SetNextTutorial(0);
		}

		private void Update()
		{
			if (!TimeManager.Instance.IsPaused())
			{
				TimeManager.Instance.Pause();
			}
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

			currentTutorial.StartTutorial(narrator, arrow);
			//explainText.text = currentTutorial.Explanation;
		}

		private void CompletedAllTutorials()
		{
			//explainText.text = "You completed all the tutorials!";
			IsActive = false;
			TimeManager.Instance.UnPause();
			Destroy(gameObject);
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