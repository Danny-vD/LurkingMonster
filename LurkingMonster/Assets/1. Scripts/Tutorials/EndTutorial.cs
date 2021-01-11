using Events;
using VDFramework.EventSystem;

namespace Tutorials
{
	public class EndTutorial : TutorialManager
	{
		protected override void Start()
		{
			EventManager.Instance.AddListener<EndGameEvent>(StartEndTutorial);
		}

		private void StartEndTutorial()
		{
			StartTutorial();
			
		}
	}
}