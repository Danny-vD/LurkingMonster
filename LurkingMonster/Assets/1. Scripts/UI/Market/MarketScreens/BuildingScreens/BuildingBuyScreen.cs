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
		private List<BuildingButtonPerBuildingType> buttonsPerBuildingType;
		
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
				tile.SpawnBuilding(false);
				manager.CloseMarket();
			}
		}
		
		private void SetupBuildingButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			buttonsPerBuildingType.ForEach(SetupBuildingButton);
		}

		private static void SetupBuildingButton(BuildingButtonPerBuildingType buttonPerBuildingType)
		{
			Button button = buttonPerBuildingType.Value;
			
			
		}

		[ContextMenu("Populate")]
		public void PopulateDictionary()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<BuildingButtonPerBuildingType, BuildingType, Button>(buttonsPerBuildingType);
		}
	}
}