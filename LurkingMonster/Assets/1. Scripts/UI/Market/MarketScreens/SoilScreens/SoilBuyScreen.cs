﻿using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.SoilScreens
{
	public class SoilBuyScreen : AbstractMarketScreen
	{
		[SerializeField]
		private Button btnBuy = null;
		
		protected override void SetScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupBuyButton(tile, manager);
		}
		
		private void SetupBuyButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: block the button if we can't affort it
			SetButton(btnBuy, tile.SpawnSoil, manager.CloseMarket);
		}
	}
}