using Events.OpenMarketEvents;
using UI.Market.MarketScreens;

namespace UI.Market.MarketManagers
{
	public class ResearchFacilityMarketManager : EventMarketManager<OpenResearchFacilityEvent>
	{
		protected override void SetupMarket(OpenResearchFacilityEvent openEvent)
		{
		}

		protected override void OnScreenFocus(AbstractMarketScreen screen)
		{
			screen.SetUI(null, this);
		}
	}
}