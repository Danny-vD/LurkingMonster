using System;
using Singletons;
using UnityEngine;
using VDFramework.Singleton;

namespace _1._Scripts.Tutorial
{
	public class TutorialManager : Singleton<TutorialManager>
	{
		[SerializeField]
		private GameObject narrator;

		[SerializeField]
		private GameObject arrow;
		
		private global::Tutorial.Tutorial[] tutorials;
		
		private global::Tutorial.Tutorial currentTutorial;

		public bool IsActive { get; private set; }

		private bool paused;

		private void Start()
		{
			if (UserSettings.SettingsExist)
			{
				Destroy(CachedGameObject);
				return;
			}
			
			IsActive  = true;
			tutorials = GetComponents<global::Tutorial.Tutorial>();
			SetNextTutorial(0);
		}

		private void Update()
		{
			if (!paused && !TimeManager.Instance.IsPaused())
			{
				paused = true;
				Invoke(nameof(PauseGame), 1.0f);
			}
		}
		
		public void CompletedTutorial()
		{
			paused = false;
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

		private global::Tutorial.Tutorial GetTutorialByOrder(int order)
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

		private void PauseGame()
		{
			TimeManager.Instance.Pause();
		}
	}
}