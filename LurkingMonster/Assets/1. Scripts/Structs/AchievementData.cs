using System;

namespace Structs
{
	[Serializable]
	public struct AchievementData
	{
		public int counter;
		public bool[] unlocked;
		public bool[] collected;

		public AchievementData(int counter, bool[] unlocked, bool[] collected)
		{
			this.counter   = counter;
			this.unlocked  = unlocked;
			this.collected = collected;
		}
	}
}