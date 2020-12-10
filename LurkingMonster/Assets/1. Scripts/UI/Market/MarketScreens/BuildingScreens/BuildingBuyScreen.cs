using System.Linq;
using Enums;
using Grid.Tiles.Buildings;
using VDFramework.Extensions;

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

		protected override BuildingType[] GetUnlockedTypes() => default(BuildingType).GetValues().ToArray();

		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetCurrentFirstTierData().Price;
	}
}