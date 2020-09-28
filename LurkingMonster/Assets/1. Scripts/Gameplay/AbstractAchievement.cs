using System.Collections.Generic;

namespace Gameplay
{
	class AbstractAchievement
	{
		private int value;
		
		private List<int> valueLimits = new List<int>();
		private List<int> experienceGained = new List<int>();


		public AbstractAchievement(int startValue, List<int> valueLimits, List<int> experienceGained)
		{
			value                 = startValue;
			this.valueLimits      = valueLimits;
			this.experienceGained = experienceGained;
		}

		public int IncreaseValue(int amount)
		{
			value += amount;

			for (int i = 0; i < valueLimits.Count; i++)
			{
				if (value == valueLimits.Count)
				{
					return experienceGained[i];
				}
			}

			return 5;
		}
	}
}