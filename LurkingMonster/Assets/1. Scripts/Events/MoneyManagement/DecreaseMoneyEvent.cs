using VDFramework.EventSystem;

namespace Events.MoneyManagement
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