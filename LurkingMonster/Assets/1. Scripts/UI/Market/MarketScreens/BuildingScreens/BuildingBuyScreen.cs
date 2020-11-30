using System.Collections.Generic;
using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Utility;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingBuyScreen : AbstractMarketScreen
	{
		[SerializeField]
		private Button btnBuy = null;

		[SerializeField]
		private List<BuildingButtonData> buildingButtonData;

		private BuildingButtonData? selectedButton;

		public List<BuildingButtonData> GetBuildingButtons()
		{
			return new List<BuildingButtonData>(buildingButtonData);
		}
		
		protected override void SetupScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupBuyButton(tile, manager);
			SetupBuildingButtons(tile, manager);
		}

		private void SetupBuyButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: block the button if we can't affort it
			SetButton(btnBuy, OnClick);

			void OnClick()
			{
				tile.SpawnBuilding(true);
				manager.CloseMarket();
			}
		}

		private void SetupBuildingButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			foreach (BuildingButtonData buildingButtonDatum in buildingButtonData)
			{
				// TODO: block the button if it's not unlocked yet
				SetButton(buildingButtonDatum.Value, () => Select(tile, buildingButtonDatum));
			}

			Select(tile, selectedButton ?? buildingButtonData[0]);
		}

		private void Select(AbstractBuildingTile tile, BuildingButtonData datum)
		{
			if (selectedButton != null && selectedButton.Value.Equals(datum))
			{
				return;
			}
			
			SetTextActive(datum, true);
			btnBuy.transform.position = datum.Value.transform.position;
			tile.SetBuildingType(datum.Key);

			Deselect(selectedButton);
			selectedButton = datum;
		}

		private static void Deselect(BuildingButtonData? datum)
		{
			if (datum != null)
			{
				SetTextActive(datum.Value, false);
			}
		}

		[ContextMenu("Populate")]
		public void PopulateDictionary()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<BuildingButtonData, BuildingType, Button>(buildingButtonData);
		}

		private static void SetTextActive(BuildingButtonData buttonData, bool active)
		{
			buttonData.Texts.Rent.gameObject.SetActive(active);
			buttonData.Texts.Health.gameObject.SetActive(active);
			buttonData.Texts.Upgrades.gameObject.SetActive(active);
		}
	}
}