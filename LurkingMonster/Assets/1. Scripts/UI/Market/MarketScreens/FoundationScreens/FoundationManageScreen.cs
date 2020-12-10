﻿using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.FoundationScreens
{
	public class FoundationManageScreen : AbstractMarketScreen
	{
		[Header("Demolish"), SerializeField]
		private Button btnDemolish = null;

		[SerializeField]
		private TextMeshProUGUI demolishText = null;

		[Header("Repair"), SerializeField]
		private Button btnRepair = null;

		[SerializeField]
		private TextMeshProUGUI repairText = null;

		protected override void SetupScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			if (tile.HasBuilding)
			{
				BlockButton(btnRepair, false);
				SetupRepairButton(tile, manager);
			}
			else
			{
				BlockButton(btnRepair, true);
			}

			SetupDemolishButton(tile, manager);
		}

		private void SetupRepairButton(AbstractBuildingTile tile, MarketManager manager)
		{
			int price = tile.GetCurrentFoundationData().RepairCost;
			repairText.text = price.ToString();

			if (!CanAffort(price))
			{
				BlockButton(btnRepair, true);
				return;
			}

			BlockButton(btnRepair, false);

			SetButton(btnRepair, OnClick);

			void OnClick()
			{
				ReduceMoney(price);
				BuildingHealth buildingHealth = tile.Building.GetComponent<BuildingHealth>();
				buildingHealth.ResetFoundationHealth();
				manager.CloseMarket();
			}
		}

		private void SetupDemolishButton(AbstractBuildingTile tile, MarketManager manager)
		{
			int price = tile.GetCurrentFoundationData().DestructionCost;
			demolishText.text = price.ToString();

			if (!CanAffort(price) || tile.HasBuilding)
			{
				BlockButton(btnDemolish, true);
				return;
			}

			BlockButton(btnDemolish, false);

			SetButton(btnDemolish, OnClick);

			void OnClick()
			{
				ReduceMoney(price);

				tile.RemoveFoundation();
				manager.CloseMarket();
			}
		}
	}
}