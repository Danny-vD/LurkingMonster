using Gameplay;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Extensions;

namespace UI.Market.MarketScreens
{
	public class MainScreen : AbstractMarketScreen
	{
		[Header("Buttons"), SerializeField]
		private Button buildingButton = null;

		[SerializeField]
		private Button foundationButton = null;

		[SerializeField]
		private Button soilButton = null;

		[Space(10), Header("Text"), SerializeField]
		private TextMeshProUGUI buildingTypeText;

		[SerializeField]
		private TextMeshProUGUI foundationTypeText;

		[SerializeField]
		private TextMeshProUGUI soilTypeText;

		[Space(10), Header("HealthBars"), SerializeField]
		private Bar buildingHealthBar;

		[SerializeField]
		private Bar foundationHealthBar;

		[SerializeField]
		private Bar soilHealthBar;

		protected override void SetScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			if (tile.HasBuilding) // Tile has building, foundation and soil
			{
				SetBars(tile);
				HasBuilding(tile, manager);
				return;
			}

			// Healthbars only matter in case of a building
			SetBars(null);

			if (tile.HasDebris) // Tile has debris
			{
				SetBars(null);
				HasDebris(tile, manager);
				return;
			}

			if (tile.HasFoundation) // Tile has foundation and soil
			{
				HasFoundation(tile, manager);
				return;
			}

			if (tile.HasSoil) // Tile has soil
			{
				HasSoil(tile, manager);
				return;
			}

			// The tile is empty
			HandleEmpty(tile, manager);
		}

		private void HasDebris(AbstractBuildingTile tile, MarketManager manager)
		{
			BlockButtons(tile, manager, true, true, true);

			SetText("Blocked by Debris!", "Blocked by Debris!", "Blocked by Debris!");

			//TODO: have some button to remove debris
		}

		private void HasBuilding(AbstractBuildingTile tile, MarketManager manager)
		{
			SetButton(buildingButton, () => manager.PutScreenInFocus(manager.Screens.BuildingManageScreen));
			SetButton(foundationButton, () => manager.PutScreenInFocus(manager.Screens.FoundationManageScreen));
			SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilManageScreen));

			SetText(tile.Building.BuildingType.ToString(), tile.GetFoundationType().ToString(), tile.GetSoilType().ToString());
		}

		private void HasFoundation(AbstractBuildingTile tile, MarketManager manager)
		{
			SetButton(buildingButton, () => manager.PutScreenInFocus(manager.Screens.BuildingBuyScreen));
			SetButton(foundationButton, () => manager.PutScreenInFocus(manager.Screens.FoundationManageScreen));
			SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilManageScreen));
			
			SetText("NoBuilding!", tile.GetFoundationType().ToString(), tile.GetSoilType().ToString());
		}

		private void HasSoil(AbstractBuildingTile tile, MarketManager manager)
		{
			BlockButtons(tile, manager, true, false, false);

			SetButton(foundationButton, () => manager.PutScreenInFocus(manager.Screens.FoundationBuyScreen));
			SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilManageScreen));
			
			SetText("NoBuilding!", "NoFoundation!", tile.GetSoilType().ToString());
		}

		private void HandleEmpty(AbstractBuildingTile tile, MarketManager manager)
		{
			BlockButtons(tile, manager, true, true, false);

			SetButton(soilButton, () => manager.PutScreenInFocus(manager.Screens.SoilBuyScreen));
			
			SetText("NoBuilding!", "NoFoundation!", "NoSoil!");
		}

		private void BlockButtons(AbstractBuildingTile tile, MarketManager manager,
			bool                                       blockBuilding,
			bool                                       blockFoundation,
			bool                                       blockSoil)
		{
			BlockBuildingButton(tile, manager, blockBuilding);

			BlockFoundationButton(tile, manager, blockFoundation);

			BlockSoilButton(tile, manager, blockSoil);
		}

		private void BlockBuildingButton(AbstractBuildingTile tile, MarketManager manager, bool blocked)
		{
			//TODO: Block the button with something....
			buildingButton.onClick.RemoveAllListeners();
		}

		private void BlockFoundationButton(AbstractBuildingTile tile, MarketManager manager, bool blocked)
		{
			//TODO: Block the button with something....
			foundationButton.onClick.RemoveAllListeners();
		}

		private void BlockSoilButton(AbstractBuildingTile tile, MarketManager manager, bool blocked)
		{
			//TODO: Block the button with something....
			soilButton.onClick.RemoveAllListeners();
		}

		private void SetBars(AbstractBuildingTile tile)
		{
			bool active = tile != null;

			buildingHealthBar.CachedGameObject.SetActive(active);
			foundationHealthBar.CachedGameObject.SetActive(active);
			soilHealthBar.CachedGameObject.SetActive(active);

			if (!active)
			{
				return;
			}

			BuildingHealth health = tile.Building.GetComponent<BuildingHealth>();

			health.SetBuildingHealthBar(buildingHealthBar);
			health.SetFoundationHealthBar(foundationHealthBar);
			health.SetSoilHealthBar(soilHealthBar);
		}

		private void SetText(string buildingText, string foundationText, string soilText)
		{
			// Use the Language file instead... perhaps use the Enum.ToString for json key?
			buildingTypeText.text   = buildingText.ReplaceUnderscoreWithSpace();
			foundationTypeText.text = foundationText.ReplaceUnderscoreWithSpace();
			soilTypeText.text       = soilText.ReplaceUnderscoreWithSpace();
		}
	}
}