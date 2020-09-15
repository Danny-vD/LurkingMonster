using VDFramework.EventSystem;

namespace Events
{
	public class CollectRentEvent : VDEvent
	{
		public readonly float Rent;

		public CollectRentEvent(float rent)
		{
			Rent = rent;
		}
	}
}
