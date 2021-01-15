using Events;
using Singletons;
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
		}
	}
}