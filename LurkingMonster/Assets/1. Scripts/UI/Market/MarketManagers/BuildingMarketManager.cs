using Events;
using Events.OpenMarketEvents;
using Grid.Tiles.Buildings;
using UI.Market.MarketScreens;

namespace UI.Market.MarketManagers
{
	public class BuildingMarketManager : EventMarketManager<OpenMarketEvent>
	{
		private AbstractBuildingTile tile;
		
		protected override void SetupMarket(OpenMarketEvent openEvent)
		{
			tile = openEvent.BuildingTile;

			if (!tile)
			{
				tile = openEvent.Building.GetComponentInParent<AbstractBuildingTile>();
			}
		}

		protected override void OnScreenFocus(AbstractMarketScreen screen)
		{
			screen.SetUI(tile, this);
		}
	}
}