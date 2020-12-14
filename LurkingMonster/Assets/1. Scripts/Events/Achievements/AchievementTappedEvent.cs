using FMOD;
using Gameplay.Achievements;
using VDFramework.EventSystem;
using Debug = UnityEngine.Debug;

namespace Events.Achievements
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