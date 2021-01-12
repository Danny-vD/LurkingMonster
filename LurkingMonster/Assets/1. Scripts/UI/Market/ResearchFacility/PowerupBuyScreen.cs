using System.Linq;
using Enums;
using Grid.Tiles.Buildings;
using UI.Market.MarketScreens;
using VDFramework.Extensions;

namespace UI.Market.ResearchFacility
{
	public class PowerupBuyScreen : AbstractMarketBuyScreen<PowerUpType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, PowerUpType buyType)
		{
		}

		protected override PowerUpType[] GetUnlockedTypes() => default(PowerUpType).GetValues().ToArray();

		protected override int GetPrice(AbstractBuildingTile tile) => 1000;

		protected override void ReduceMoney(int price)
		{
			//TODO: Decrease soil samples
		}
	}
}