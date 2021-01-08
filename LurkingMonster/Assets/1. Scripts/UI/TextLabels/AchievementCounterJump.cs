using Events.Achievements;
using Singletons;
using UI.Bounce;

namespace UI.TextLabels
{
	public class AchievementCounterJump : AbstractBounce<AchievementUnlockedEvent>
	{
		protected override void Start()
		{
		}

		private void Update()
		{
			if (RewardManager.Instance.Counter > 0 && gameObject.activeInHierarchy && !IsRunning)
			{
				StartBounce();
			}
		}
	}
}