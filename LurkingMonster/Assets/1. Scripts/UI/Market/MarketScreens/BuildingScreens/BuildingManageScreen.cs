using System.Collections.Generic;
using System.Globalization;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingManageScreen : AbstractMarketScreen
	{
		[Header("Upgrade"), SerializeField]
		private List<Button> btnUpgrade = null;

		[SerializeField]
		private TextMeshProUGUI upgradeText = null;

		[Header("Demolish"), SerializeField]
		private Button btnDemolish = null;

		[SerializeField]
		private TextMeshProUGUI demolishText = null;

		[Header("Repair"), SerializeField]
		private Button btnRepair = null;

		[SerializeField]
		private TextMeshProUGUI repairText = null;

		[Header("Builing Stats"), SerializeField]
		private TextMeshProUGUI currentTierHealth = null;

		[SerializeField]
		private TextMeshProUGUI currentTierRent = null;

		[SerializeField]
		private TextMeshProUGUI nextTierHealth = null;

		[SerializeField]
		private TextMeshProUGUI nextTierRent = null;

		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupUpgradeButtons(tile, manager);
			SetupRepairButton(tile, manager);
			SetupDemolishButton(tile, manager);
		}

		private void SetupUpgradeButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			BuildingUpgrade buildingUpgrade = tile.Building.GetComponent<BuildingUpgrade>();

			SetTierText(tile, buildingUpgrade);
			
			if (buildingUpgrade.CanUpgrade())
			{
				btnUpgrade.ForEach(Setup);

				upgradeText.text = tile.Building.UpgradeCost.ToString();
				return;
			}
			
			//TODO: Change
			upgradeText.text = "MAX UPGRADE";

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
			repairText.text = tile.Building.Data.RepairPrice.ToString();
			SetButton(btnRepair, buildingHealth.ResetBuildingHealth, manager.CloseMarket);
		}

		private void SetupDemolishButton(AbstractBuildingTile tile, MarketManager manager)
		{
			SetButton(btnDemolish, OnClick);

			demolishText.text = tile.Building.Data.DestructionCost.ToString();

			void OnClick()
			{
				tile.Building.RemoveBuilding(true);
				manager.CloseMarket();
			}
		}

		private void SetTierText(AbstractBuildingTile tile, BuildingUpgrade buildingUpgrade)
		{
			currentTierHealth.text = ((int) tile.Building.Data.MaxHealth).ToString();
			currentTierRent.text   = tile.Building.Data.Rent.ToString();
			
			if (buildingUpgrade.CanUpgrade())
			{
				nextTierHealth.text = ((int) tile.Building.NextTierData.MaxHealth).ToString();
				nextTierRent.text   = tile.Building.NextTierData.Rent.ToString();
				return;
			}

			nextTierHealth.text = currentTierHealth.text;
			nextTierRent.text   = currentTierRent.text;
		}
	}
}