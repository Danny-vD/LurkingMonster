using VDFramework.EventSystem;

namespace Events.MoneyManagement
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