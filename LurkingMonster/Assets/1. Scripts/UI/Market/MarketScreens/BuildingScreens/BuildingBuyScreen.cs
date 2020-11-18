﻿using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingBuyScreen : AbstractMarketScreen
	{
		[SerializeField]
		private Button btnBuy = null;
		
		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupBuyButton(tile, manager);
		}

		private void SetupBuyButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: block the button if we can't affort it
			SetButton(btnBuy, tile.SpawnBuilding, manager.CloseMarket);
		}
	}
}