using VDFramework.EventSystem;

namespace Events
{
	public class MoneyChangedEvent : VDEvent
	{
		public readonly int CurrentMoney;
		public readonly int DeltaMoney;

		public MoneyChangedEvent(int currentMoney, int deltaMoney)
		{
			CurrentMoney = currentMoney;
			DeltaMoney = deltaMoney;
		}
	}
}