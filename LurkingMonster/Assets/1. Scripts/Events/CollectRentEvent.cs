using VDFramework.EventSystem;

namespace Events
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
