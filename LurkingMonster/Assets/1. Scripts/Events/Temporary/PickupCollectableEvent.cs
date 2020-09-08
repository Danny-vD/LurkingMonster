using Temporary;
using VDFramework.EventSystem;

namespace Events.Temporary
{
	public class PickupCollectableEvent : VDEvent
	{
		public readonly Collectable Collectable;

		public PickupCollectableEvent(Collectable collectable)
		{
			Collectable = collectable;
		}
	}
}