using Gameplay;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens
{
	public class MainScreen : AbstractMarketScreen
	{
		[SerializeField]
		private Button buildingButton = null;

		[SerializeField]
		private Button foundationButton = null;

		[SerializeField]
		private Button soilButton = null;

		[Space(10), SerializeField]
		private Bar buildingHealth;

		[SerializeField]
		private Bar foundationHealth;

		[SerializeField]
		private Bar soilHealth;
		
		//TODO: Split it up differently.. so that the checks are only needed once
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
				BlockBuildingButton(tile, manager);
				return;
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
				BlockFoundationButton(tile, manager);
				return;
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
				BlockSoilButton(tile, manager);
				return;
			}

			if (tile.HasSoil)
			{
				SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilManageScreen));
				return;
			}

			SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilBuyScreen));
		}
		
		private void BlockBuildingButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: Block the button with something....
			buildingButton.onClick.RemoveAllListeners();
			buildingHealth.CachedGameObject.SetActive(false);
		}
		
		private void BlockFoundationButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: Block the button with something....
			foundationButton.onClick.RemoveAllListeners();
			foundationHealth.CachedGameObject.SetActive(false);
		}
		
		private void BlockSoilButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: Block the button with something....
			soilButton.onClick.RemoveAllListeners();
			soilHealth.CachedGameObject.SetActive(false);
		}
	}
}