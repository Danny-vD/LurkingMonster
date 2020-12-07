using Enums;
using Grid.Tiles.Buildings;

namespace UI.Market.MarketScreens.SoilScreens
{
	public class SoilBuyScreen : AbstractMarketBuyScreen<SoilType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, SoilType buyType)
		{
			tile.SetSoilType(buyType);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			tile.SpawnSoil();
			base.BuyButtonClick(tile, manager);
		}
		
		protected override SoilType[] GetUnlockedTypes() => new SoilType[0];

		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetCurrentSoilData().BuildCost;
	}
}