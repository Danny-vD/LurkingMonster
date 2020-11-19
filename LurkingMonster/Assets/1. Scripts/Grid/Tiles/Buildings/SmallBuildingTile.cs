using System;
using Enums.Grid;
using Events;
using VDFramework.EventSystem;

namespace Grid.Tiles.Buildings
{
	public class SmallBuildingTile : AbstractBuildingTile
	{
		public override TileType TileType => TileType.SmallBuilding;
	}
}