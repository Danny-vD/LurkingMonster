using Events;
using Singletons;
using UI;
using UnityEngine;
using VDFramework.EventSystem;

namespace Tutorials
{
	public class EndTutorial : TutorialManager
	{
		protected override void Start()
		{
			if (UserSettings.GameData.ETutorial)
			{
				return;
			}
			
			EventManager.Instance.AddListener<EndGameEvent>(StartEndTutorial);
		}

		private void StartEndTutorial()
		{
			StartTutorial();
		}

		protected override void CompletedAllTutorials()
		{
			base.CompletedAllTutorials();
			UserSettings.GameData.ETutorial = true;
			
			// Close the application after the player finishes to give them the choice to continue or just stop playing
			Application.Quit();
		}
	}
}