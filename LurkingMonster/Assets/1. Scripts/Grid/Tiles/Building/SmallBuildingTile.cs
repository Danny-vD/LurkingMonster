using Enums.Grid;
using Events;
using VDFramework.EventSystem;

namespace Grid.Tiles.Building
{
	public class SmallBuildingTile : AbstractBuildingTile
	{
		public override TileType TileType => TileType.SmallBuilding;

		public override void SpawnBuilding()
		{
			base.SpawnBuilding();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(BuildingData.Price));
		}
	}
}