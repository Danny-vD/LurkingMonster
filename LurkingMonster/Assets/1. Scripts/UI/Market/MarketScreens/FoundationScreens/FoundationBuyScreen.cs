using Enums;
using Grid.Tiles.Buildings;

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
		
		protected override FoundationType[] GetUnlockedTypes() => new FoundationType[0];
		
		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetCurrentFoundationData().BuildCost;
	}
}