using Gameplay.Buildings;
using VDFramework.EventSystem;

namespace Events
{
	public class BuildingConsumedEvent : VDEvent
	{
		public readonly Building Building;

		public BuildingConsumedEvent(Building building)
		{
			Building = building;
		}
	}
}