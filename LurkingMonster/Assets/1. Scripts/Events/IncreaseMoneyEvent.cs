using VDFramework.EventSystem;

namespace Events
{
	public class IncreaseMoneyEvent : VDEvent<IncreaseMoneyEvent>
	{
		public readonly int Amount;

		public IncreaseMoneyEvent(int amount)
		{
			Amount = amount;
		}
	}
}