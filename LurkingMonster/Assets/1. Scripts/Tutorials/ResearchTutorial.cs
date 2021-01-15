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
			
			EventManager.Instance.AddListener<OpenResearchFacilityEvent>(StartTutorial);
		}

		protected override void CompletedAllTutorials()
		{
			IsActive = false;
			DisableNarrator();
			
			Tutorial.ResetIndex();
			Destroy(gameObject);
			
			UserSettings.GameData.RTutorial = true;
			
			EventManager.Instance.RemoveListener<OpenResearchFacilityEvent>(StartTutorial);
		}
	}
}