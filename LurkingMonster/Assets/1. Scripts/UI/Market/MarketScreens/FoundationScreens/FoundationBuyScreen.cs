using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UnityEngine;

namespace UI.Market.MarketScreens.FoundationScreens
{
	public class FoundationBuyScreen : AbstractMarketBuyScreen<FoundationType, FoundationButtonData>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, FoundationButtonData data)
		{
			tile.SetFoundationType(data.Key);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			tile.SpawnFoundation();
			base.BuyButtonClick(tile, manager);
		}
	}
}