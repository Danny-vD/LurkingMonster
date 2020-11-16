using System;
using Enums.Grid;
using Events;
using VDFramework.EventSystem;

namespace Grid.Tiles.Buildings
{
	using Singletons;

	public class SmallBuildingTile : AbstractBuildingTile
	{
		public override TileType TileType => TileType.SmallBuilding;

		public override void SpawnSoil()
		{
			int cost = GetSoilPrice();

			if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
			{
				return;
			}

			base.SpawnSoil();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
		}

		public override void SpawnFoundation()
		{
			int cost = GetFoundationPrice();

			if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
			{
				return;
			}

			base.SpawnFoundation();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
		}
		
		public override void SpawnBuilding()
		{
			int cost = GetBuildingPrice();
			
			if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
			{
				return;
			}

			base.SpawnBuilding();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
		}

		public override void RemoveSoil(bool payForRemoval)
		{
			if (payForRemoval)
			{
				int cost = GetSoilData(GetSoilType()).RemoveCost;

				if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
				{
					return;
				}

				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
			}

			base.RemoveSoil(payForRemoval);
		}
		
		public override void RemoveFoundation(bool payForRemoval)
		{
			if (payForRemoval)
			{
				int cost = GetFoundationData(GetFoundationType()).DestroyCost;

				if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
				{
					return;
				}

				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
			}

			base.RemoveFoundation(payForRemoval);
		}

		public override void RemoveDebris(bool payForRemoval)
		{
			if (payForRemoval)
			{
				int cost = DestroyedBuildingData.CleanupCosts;

				if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
				{
					return;
				}
				
				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
			}
			
			base.RemoveDebris(payForRemoval);
		}
	}
}