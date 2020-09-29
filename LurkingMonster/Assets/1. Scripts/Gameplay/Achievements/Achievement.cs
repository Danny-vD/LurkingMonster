using System;

namespace Gameplay.Achievements
{
	public class Achievement
	{
		private readonly int[] experiences;
		private readonly int[] limits;
		private readonly bool[] unlocked;

		public static LevelBar LevelBar;

		public Achievement(int[] experiences, int[] limits)
		{
			this.experiences = experiences;
			this.limits      = limits;

			unlocked = new bool[experiences.Length];
		}

		public void CheckAchievement(int counter)
		{
			for (int i = 0; i < unlocked.Length; i++)
			{
				if (!unlocked[i])
				{
					if (counter >= limits[i])
					{
						unlocked[i] = true;
						LevelBar.AddExperience(experiences[i]);
						
						//TODO show achievement!!
						return;
					}
				}
			}
		}
	}
}