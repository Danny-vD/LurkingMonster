using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens
{
	public class MainScreen : AbstractMarketScreen
	{
		[SerializeField]
		private Button buildingButton;

		[SerializeField]
		private Button foundationButton;

		[SerializeField]
		private Button soilButton;

		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupBuildingButton(tile, manager);
			SetupFoundationButton(tile, manager);
			SetupSoilButton(tile, manager);
		}

		private void SetupBuildingButton(AbstractBuildingTile tile, MarketManager manager)
		{
			if (tile.HasDebris || !tile.HasFoundation || !tile.HasSoil)
			{
				//TODO: Block the button with something....
				buildingButton.onClick.RemoveAllListeners();
			}

			if (tile.Building)
			{
				SetButton(buildingButton, () => manager.PutScreenInFocus(manager.Screens.BuildingManageScreen));
				return;
			}

			SetButton(buildingButton, () => manager.PutScreenInFocus(manager.Screens.BuildingBuyScreen));
		}

		private void SetupFoundationButton(AbstractBuildingTile tile, MarketManager manager)
		{
			if (tile.HasDebris || !tile.HasSoil)
			{
				//TODO: Block the button with something....
				foundationButton.onClick.RemoveAllListeners();
			}

			if (tile.HasFoundation)
			{
				SetButton(foundationButton, () => manager.PutScreenInFocus(manager.Screens.FoundationManageScreen));
				return;
			}

			SetButton(foundationButton, () => manager.PutScreenInFocus(manager.Screens.FoundationBuyScreen));
		}

		private void SetupSoilButton(AbstractBuildingTile tile, MarketManager manager)
		{
			if (tile.HasDebris)
			{
				//TODO: Block the button with something....
				soilButton.onClick.RemoveAllListeners();
			}

			if (tile.HasSoil)
			{
				SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilManageScreen));
				return;
			}

			SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilBuyScreen));
		}
	}
}