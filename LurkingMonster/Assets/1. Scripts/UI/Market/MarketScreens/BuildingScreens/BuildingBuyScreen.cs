using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UnityEngine;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingBuyScreen : AbstractMarketBuyScreen<BuildingType, BuildingButtonData>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, BuildingButtonData data)
		{
			tile.SetBuildingType(data.Key);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			tile.SpawnBuilding(true);
			base.BuyButtonClick(tile, manager);
		}
	}
}