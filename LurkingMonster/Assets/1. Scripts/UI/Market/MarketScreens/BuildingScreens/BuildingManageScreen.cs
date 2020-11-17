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
		private List<Button> btnUpgrade;

		[Space(20), SerializeField]
		private Button btnDemolish;
	
		[SerializeField]
		private Button btnRepair;
		
		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupUpgradeButtons(tile, manager);
			SetupRepairButton(tile, manager);
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
				}
			}
		}
		
		private void SetupRepairButton(AbstractBuildingTile tile, MarketManager manager)
		{
			BuildingHealth buildingHealth = tile.Building.GetComponent<BuildingHealth>();
			SetButton(btnRepair, buildingHealth.ResetBuildingHealth);
		}
	}
}