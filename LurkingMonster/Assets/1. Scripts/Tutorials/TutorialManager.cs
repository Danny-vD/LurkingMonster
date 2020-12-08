using System;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Singleton;

namespace Tutorials
{
	public class TutorialManager : Singleton<TutorialManager>
	{
		[SerializeField]
		private GameObject narrator;

		[SerializeField]
		private GameObject arrow;

		[SerializeField]
		private Button exit;
		
		private Tutorial[] tutorials;
		
		private Tutorial currentTutorial;

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
			tutorials = GetComponents<Tutorial>();
			SetNextTutorial(0);
			exit.onClick.AddListener(CompletedAllTutorials);
		}

		private void Update()
		{
			if (!paused && !TimeManager.Instance.IsPaused())
			{
				PauseGame();
			}
		}
		
		public void CompletedTutorial()
		{
			paused = false;
			SetNextTutorial(currentTutorial.Order + 1);
		}
		
		
		private void CompletedAllTutorials()
		{
			//explainText.text = "You completed all the tutorials!";
			IsActive = false;
			TimeManager.Instance.UnPause();
			print("Nke");
			narrator.SetActive(false);
			Destroy(gameObject);
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

		private void PauseGame()
		{
			TimeManager.Instance.Pause();
		}
	}
}