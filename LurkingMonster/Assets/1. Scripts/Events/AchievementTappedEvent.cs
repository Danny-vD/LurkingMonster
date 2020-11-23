using Gameplay.Achievements;
using VDFramework.EventSystem;

namespace Events
{
	public class AchievementTappedEvent : VDEvent<AchievementTappedEvent>
	{
		public readonly Achievement Achievement;

		public AchievementTappedEvent(Achievement achievement)
		{
			this.Achievement = achievement;
		}
	}
}