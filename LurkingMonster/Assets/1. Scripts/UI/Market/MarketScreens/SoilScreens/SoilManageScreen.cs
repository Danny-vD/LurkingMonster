using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using TMPro;
using UI.Market.MarketManagers;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.SoilScreens
{
	public class SoilManageScreen : AbstractMarketScreen
	{
		[Header("Demolish"), SerializeField]
		private Button btnDemolish = null;

		[SerializeField]
		private TextMeshProUGUI demolishText = null;
		
		[SerializeField]
		private ConfirmPopup confirmDemolishPopup;

		[Header("Repair"), SerializeField]
		private Button btnRepair = null;

		[SerializeField]
		private TextMeshProUGUI repairText = null;

		protected override void SetupScreen(AbstractBuildingTile tile, AbstractMarketManager manager)
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

		private void SetupRepairButton(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			int price = tile.GetCurrentSoilData().RepairCost;
			BuildingHealth buildingHealth = tile.Building.GetComponent<BuildingHealth>();
			
			float percentage = buildingHealth.CurrentSoilHealthPercentage;

			price = (int) ((1 - percentage) * price);
			
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
				buildingHealth.ResetSoilHealth();
				manager.CloseMarket();
			}
		}

		private void SetupDemolishButton(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			int price = tile.GetCurrentSoilData().RemoveCost;
			demolishText.text = price.ToString();

			if (!CanAffort(price) || tile.HasFoundation)
			{
				BlockButton(btnDemolish, true);
				return;
			}

			BlockButton(btnDemolish, false);

			SetButton(btnDemolish, OnClick);
			
			void OnClick()
			{
				confirmDemolishPopup.ShowPopUp(OnConfirmDemolish);
			}

			void OnConfirmDemolish()
			{
				ReduceMoney(price);
				tile.RemoveSoil();
				manager.CloseMarket();
			}
		}
	}
}