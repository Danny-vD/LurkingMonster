using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens
{
	public class BuildingBuyScreen : AbstractMarketScreen
	{
		[SerializeField]
		private Button buyButton;
		
		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupBuyButton(tile, manager);
		}

		private void SetupBuyButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: block the button if we can't affort it
			SetButton(buyButton, tile.SpawnBuilding, manager.CloseMarket);
		}
	}
}