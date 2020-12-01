using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UnityEngine;

namespace UI.Market.MarketScreens.SoilScreens
{
	public class SoilBuyScreen : AbstractMarketBuyScreen<SoilType, SoilButtonData>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, SoilButtonData data)
		{
			tile.SetSoilType(data.Key);
		}

		protected override void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			tile.SpawnSoil();
			base.BuyButtonClick(tile, manager);
		}

		[ContextMenu("Populate")]
		private void Populate()
		{
			PopulateDictionary();
		}
	}
}