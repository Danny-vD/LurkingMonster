﻿using Enums.Grid;
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
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(FirstTierData.Price));
		}

		public override void SpawnFoundation()
		{
			int cost = GetFoundationData(GetFoundationType()).BuildCost;

			if (!MoneyManager.Instance.PlayerHasEnoughMoney(cost))
			{
				return;
			}

			base.SpawnFoundation();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(cost));
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

			base.RemoveFoundation(false);
		}
	}
}