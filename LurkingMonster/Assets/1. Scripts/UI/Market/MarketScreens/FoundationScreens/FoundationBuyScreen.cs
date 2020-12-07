using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UnityEngine;

namespace UI.Market.MarketScreens.FoundationScreens
{
	public class FoundationBuyScreen : AbstractMarketBuyScreen<FoundationType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, FoundationType buyType)
		{
			tile.SetFoundationType(buyType);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			tile.SpawnFoundation();
			base.BuyButtonClick(tile, manager);
		}
	}
}