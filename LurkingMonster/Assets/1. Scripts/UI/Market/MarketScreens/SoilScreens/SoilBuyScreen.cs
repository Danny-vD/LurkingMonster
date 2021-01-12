using System.Linq;
using Enums;
using Events;
using Grid.Tiles.Buildings;
using UI.Market.MarketManagers;
using VDFramework.EventSystem;
using VDFramework.Extensions;

namespace UI.Market.MarketScreens.SoilScreens
{
	public class SoilBuyScreen : AbstractMarketBuyScreen<SoilType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, SoilType buyType)
		{
			tile.SetSoilType(buyType);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			EventManager.Instance.RaiseEvent(new BuyPlotEvent());
			tile.SpawnSoil();
			base.BuyButtonClick(tile, manager);
		}
		
		protected override SoilType[] GetUnlockedTypes() => default(SoilType).GetValues().ToArray();

		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetSoilPrice();
	}
}