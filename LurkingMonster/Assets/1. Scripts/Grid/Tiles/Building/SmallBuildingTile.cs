using Enums.Grid;
using Events;
using VDFramework.EventSystem;

namespace Grid.Tiles.Building
{
	using Singletons;

	public class SmallBuildingTile : AbstractBuildingTile
	{
		public override TileType TileType => TileType.SmallBuilding;

		public override void SpawnBuilding()
		{
			if (!MoneyManager.Instance.PlayerHasEnoughMoney(GetBuildingPrice()))
			{
				return;
			}

			base.SpawnBuilding();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(BuildingData.Price));
		}

		public override void SpawnFoundation()
		{
			int cost = GetFoundationData(foundationType).BuildCost;
			
			if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
			{
				return;
			}
			
			base.SpawnFoundation();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
		}

		public override void RemoveFoundation()
		{
			int cost = GetFoundationData(foundationType).DestroyCost;
			
			if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
			{
				return;
			}
			
			base.RemoveFoundation();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
		}
	}
}