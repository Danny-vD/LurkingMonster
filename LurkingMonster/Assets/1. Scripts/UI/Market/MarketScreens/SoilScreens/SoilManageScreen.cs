using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.EventSystem;

namespace UI.Market.MarketScreens.SoilScreens
{
	public class SoilManageScreen : AbstractMarketScreen
	{
		[Header("Demolish"), SerializeField]
		private Button btnDemolish = null;

		[SerializeField]
		private TextMeshProUGUI demolishText = null;

		[Header("Repair"), SerializeField]
		private Button btnRepair = null;

		[SerializeField]
		private TextMeshProUGUI repairText = null;

		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			if (tile.HasBuilding)
			{
				SetupRepairButton(tile, manager);
			}

			SetupDemolishButton(tile, manager);
		}

		private void SetupRepairButton(AbstractBuildingTile tile, MarketManager manager)
		{
			BuildingHealth buildingHealth = tile.Building.GetComponent<BuildingHealth>();
			repairText.text = tile.GetCurrentSoilData().RepairCost.ToString();
			SetButton(btnRepair, buildingHealth.ResetSoilHealth, manager.CloseMarket);
		}

		private void SetupDemolishButton(AbstractBuildingTile tile, MarketManager manager)
		{
			SetButton(btnDemolish, OnClick);

			demolishText.text = tile.GetCurrentSoilData().RemoveCost.ToString();

			void OnClick()
			{
				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(100));
				tile.RemoveSoil();
				manager.CloseMarket();
			}
		}
	}
}