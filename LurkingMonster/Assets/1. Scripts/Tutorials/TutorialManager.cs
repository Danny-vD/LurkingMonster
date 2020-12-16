using System;
using Events.MoneyManagement;
using Singletons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VDFramework.EventSystem;
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
		private Button btn_stopTutorial;

		private Tutorial[] tutorials;

		private Tutorial currentTutorial;

		public bool IsActive { get; private set; }

		private bool paused;

		public GameObject Narrator => narrator;

		public Button StopTutorialButton => btn_stopTutorial;

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
			btn_stopTutorial.onClick.AddListener(CompletedAllTutorials);
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

		public void EnableNarrator()
		{
			narrator.gameObject.SetActive(true);
		}

		public void DisableNarrator()
		{
			narrator.gameObject.SetActive(false);
		}

		private void CompletedAllTutorials()
		{
			IsActive = false;
			TimeManager.Instance.UnPause();
			narrator.SetActive(false);
			
			//TODO: make it adjustable
			EventManager.Instance.RaiseEvent(new IncreaseMoneyEvent(1000));
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

			currentTutorial.StartTutorial(arrow);
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

		private static void PauseGame()
		{
			TimeManager.Instance.Pause();
		}
	}
}