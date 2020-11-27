using VDFramework.EventSystem;

namespace Events.MoneyManagement
{
	public class CollectRentEvent : VDEvent<CollectRentEvent>
	{
		public readonly int Rent;

		public CollectRentEvent(int rent)
		{
			Rent = rent;
		}
	}
}
