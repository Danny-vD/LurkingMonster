using Gameplay.Buildings;
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
			repairText.text = tile.GetCurrentFoundationData().RepairCost.ToString();
			SetButton(btnRepair, buildingHealth.ResetFoundationHealth, manager.CloseMarket);
		}

		private void SetupDemolishButton(AbstractBuildingTile tile, MarketManager manager)
		{
			SetButton(btnDemolish, OnClick);

			demolishText.text = tile.GetCurrentFoundationData().DestructionCost.ToString();

			void OnClick()
			{
				tile.RemoveFoundation();
				manager.CloseMarket();
			}
		}
	}
}