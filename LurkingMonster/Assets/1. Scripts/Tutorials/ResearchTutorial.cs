using Events;
using Events.OpenMarketEvents;
using Singletons;
using VDFramework.EventSystem;

namespace Tutorials
{
	public class ResearchTutorial : TutorialManager
	{
		protected override void Start()
		{
			if (UserSettings.GameData.RTutorial)
			{
				return;
			}
			
			EventManager.Instance.AddListener<OpenResearchFacilityEvent>(StartEndTutorial);
		}

		private void StartEndTutorial()
		{
			StartTutorial();
		}

		protected override void CompletedAllTutorials()
		{
			base.CompletedAllTutorials();
			UserSettings.GameData.RTutorial = true;
		}
	}
}