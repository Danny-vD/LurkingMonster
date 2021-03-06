﻿using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using VDFramework.EventSystem;

namespace Events.OpenMarketEvents
{
	public class OpenMarketEvent : VDEvent<OpenMarketEvent>
	{
		public readonly AbstractBuildingTile BuildingTile;
		public readonly Building Building;
		
		public OpenMarketEvent(AbstractBuildingTile buildingTile)
		{
			BuildingTile = buildingTile;
		}

		public OpenMarketEvent(Building building)
		{
			Building = building;
		}
	}
}