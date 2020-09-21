using VDFramework.EventSystem;

namespace Events
{
	public class DecreaseMoneyEvent : VDEvent
	{
		public readonly int Amount;

		public DecreaseMoneyEvent(int amount)
		{
			Amount = amount;
		}
	}
}