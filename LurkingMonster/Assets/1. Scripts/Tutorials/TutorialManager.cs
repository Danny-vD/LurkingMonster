using Events.MoneyManagement;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace Tutorials
{
	public abstract class TutorialManager : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject narrator;

		[SerializeField]
		private GameObject arrow;

		[SerializeField]
		protected Button btn_stopTutorial;
		
		[SerializeField]
		private string suffix;

		[SerializeField]
		private int money;

		protected Tutorial[] tutorials;

		private Tutorial currentTutorial;

		public static bool IsActive { get; protected set; }

		public string Suffix => suffix;

		private bool paused;
		
		public GameObject Narrator => narrator;

		public Button StopTutorialButton => btn_stopTutorial;

		protected virtual void Start()
		{
			if (UserSettings.SettingsExist)
			{
				Destroy(CachedGameObject);
				return;
			}
			
			StartTutorial();
		}

		private void Update()
		{
			if (!IsActive)
			{
				return;
			}
			
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

		protected void StartTutorial()
		{
			IsActive     = true;
			tutorials    = GetComponents<Tutorial>();
			SetNextTutorial(0);
			btn_stopTutorial.onClick.AddListener(CompletedAllTutorials);
		}

		private void CompletedAllTutorials()
		{
			IsActive = false;
			TimeManager.Instance.UnPause();
			narrator.SetActive(false);
			
			Tutorial.ResetIndex();

			EventManager.Instance.RaiseEvent(new IncreaseMoneyEvent(money));
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

			currentTutorial.StartTutorial(arrow, this);
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