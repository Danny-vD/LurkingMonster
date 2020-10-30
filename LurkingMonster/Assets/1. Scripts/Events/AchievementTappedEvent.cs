using Gameplay.Achievements;
using VDFramework.EventSystem;

namespace Events
{
	public class AchievementTappedEvent : VDEvent
	{
		public readonly Achievement Achievement;

		public AchievementTappedEvent(Achievement achievement)
		{
			this.Achievement = achievement;
		}
	}
}