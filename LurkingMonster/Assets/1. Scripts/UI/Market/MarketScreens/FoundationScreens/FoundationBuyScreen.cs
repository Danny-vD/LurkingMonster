using System.Linq;
using Enums;
using Grid.Tiles.Buildings;
using VDFramework.Extensions;

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

		protected override FoundationType[] GetUnlockedTypes() => default(FoundationType).GetValues().ToArray();
		
		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetCurrentFoundationData().BuildCost;
	}
}