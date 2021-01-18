using System.Linq;
using Enums;
using Events.BuildingEvents;
using Grid.Tiles.Buildings;
using UI.Market.MarketManagers;
using VDFramework.EventSystem;
using VDFramework.Extensions;

namespace UI.Market.MarketScreens.FoundationScreens
{
	public class FoundationBuyScreen : AbstractMarketBuyScreen<FoundationType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, FoundationType buyType)
		{
			tile.SetFoundationType(buyType);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			tile.SpawnFoundation();
			base.BuyButtonClick(tile, manager);
			EventManager.Instance.RaiseEvent(new FoundationBuildEvent());
		}

		protected override FoundationType[] GetUnlockedTypes() => default(FoundationType).GetValues().ToArray();

		protected override int GetPrice(AbstractBuildingTile tile) => tile.GetFoundationPrice();
	}
}