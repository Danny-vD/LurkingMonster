using System.Collections.Generic;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingManageScreen : AbstractMarketScreen
	{
		[SerializeField]
		private List<Button> btnUpgrade = null;

		[Space(20), SerializeField]
		private Button btnDemolish = null;
	
		[SerializeField]
		private Button btnRepair = null;
		
		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupUpgradeButtons(tile, manager);
			SetupRepairButton(tile, manager);
			SetupDemolishButton(tile, manager);
		}

		private void SetupUpgradeButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			BuildingUpgrade buildingUpgrade = tile.Building.GetComponent<BuildingUpgrade>();
			btnUpgrade.ForEach(Setup);

			void Setup(Button button)
			{
				SetButton(button, OnClick);

				void OnClick()
				{
					buildingUpgrade.Upgrade(true);
					manager.CloseMarket();
				}
			}
		}
		
		private void SetupRepairButton(AbstractBuildingTile tile, MarketManager manager)
		{
			BuildingHealth buildingHealth = tile.Building.GetComponent<BuildingHealth>();
			SetButton(btnRepair, buildingHealth.ResetBuildingHealth, manager.CloseMarket);
		}
		
		private void SetupDemolishButton(AbstractBuildingTile tile, MarketManager manager)
		{
			SetButton(btnDemolish, OnClick);

			void OnClick()
			{
				tile.Building.RemoveBuilding(true);
				manager.CloseMarket();
			}
		}
	}
}