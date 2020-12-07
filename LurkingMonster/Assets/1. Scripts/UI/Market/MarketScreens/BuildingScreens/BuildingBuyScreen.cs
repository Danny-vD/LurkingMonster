using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UnityEngine;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingBuyScreen : AbstractMarketBuyScreen<BuildingType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, BuildingType buyType)
		{
			tile.SetBuildingType(buyType);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			tile.SpawnBuilding(true);
			base.BuyButtonClick(tile, manager);
		}
	}
}