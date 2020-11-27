using Gameplay.Buildings;
using VDFramework.EventSystem;

namespace Events.BuildingEvents
{
	public class BuildingDestroyedEvent : VDEvent<BuildingDestroyedEvent>
	{
		public readonly Building Building;

		public BuildingDestroyedEvent(Building building)
		{
			Building = building;
		}
	}
}