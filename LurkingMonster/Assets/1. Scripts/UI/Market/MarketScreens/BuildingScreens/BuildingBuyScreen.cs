using System.Linq;
using Enums;
using Grid.Tiles.Buildings;
using Singletons;
using UI.Market.MarketManagers;
using VDFramework.Extensions;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingBuyScreen : AbstractMarketBuyScreen<BuildingType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, BuildingType buyType)
		{
			tile.SetBuildingType(buyType);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			tile.SpawnBuilding(true);
			base.BuyButtonClick(tile, manager);
		}

		protected override BuildingType[] GetUnlockedTypes() => new[] {BuildingType.House, BuildingType.Store};

		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetBuildingPrice();
	}
}