using VDFramework.EventSystem;

namespace Events
{
	public class DecreaseMoneyEvent : VDEvent<DecreaseMoneyEvent>
	{
		public readonly int Amount;

		public DecreaseMoneyEvent(int amount)
		{
			Amount = amount;
		}
	}
}