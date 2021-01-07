using System;
using System.Collections.Generic;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingManageScreen : AbstractMarketScreen
	{
		private const string maxUpgradeKey = "MAX_UPGRADED";
		
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

		protected override void SetupScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupUpgradeButtons(tile, manager);
			SetupRepairButton(tile, manager);
			SetupDemolishButton(tile, manager);
		}

		private void SetupUpgradeButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			BuildingUpgrade buildingUpgrade = tile.Building.GetComponent<BuildingUpgrade>();

			SetTierText(tile, buildingUpgrade);

			if (!buildingUpgrade.CanUpgrade())
			{
				btnUpgrade.ForEach(BlockButton);

				upgradeText.text = LanguageUtil.GetJsonString(maxUpgradeKey);

				return;
			}

			int price = tile.Building.UpgradeCost;
			upgradeText.text = price.ToString();

			if (!CanAffort(price))
			{
				btnUpgrade.ForEach(BlockButton);
				return;
			}

			btnUpgrade.ForEach(UnblockButton);
			btnUpgrade.ForEach(Setup);

			void Setup(Button button)
			{
				SetButton(button, OnClick);

				void OnClick()
				{
					ReduceMoney(price);
					buildingUpgrade.Upgrade();
					manager.CloseMarket();
				}
			}
		}

		private void SetupRepairButton(AbstractBuildingTile tile, MarketManager manager)
		{
			int price = tile.Building.Data.RepairCost;
			repairText.text = price.ToString();

			if (!CanAffort(price))
			{
				BlockButton(btnRepair);
				return;
			}
			
			UnblockButton(btnRepair);
			
			SetButton(btnRepair, OnClick);

			void OnClick()
			{
				ReduceMoney(price);
				BuildingHealth buildingHealth = tile.Building.GetComponent<BuildingHealth>();
				buildingHealth.ResetBuildingHealth();
				manager.CloseMarket();
			}
		}

		private void SetupDemolishButton(AbstractBuildingTile tile, MarketManager manager)
		{
			int price = tile.Building.Data.DestructionCost;
			demolishText.text = price.ToString();

			if (!CanAffort(price))
			{
				BlockButton(btnDemolish);
				return;
			}
			
			UnblockButton(btnDemolish);
			
			SetButton(btnDemolish, OnClick);

			void OnClick()
			{
				tile.Building.RemoveBuilding();
				tile.SpawnSoil();
				tile.SpawnFoundation();
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

		private static void BlockButton(Button button)
		{
			BlockButton(button, true);
		}

		private static void UnblockButton(Button button)
		{
			BlockButton(button, false);
		}
	}
}