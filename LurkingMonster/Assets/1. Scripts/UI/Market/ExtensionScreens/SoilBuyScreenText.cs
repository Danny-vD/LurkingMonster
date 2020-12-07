using System.Collections.Generic;
using Enums;
using Grid.Tiles.Buildings;
using ScriptableObjects;
using Structs.Market;
using UI.Market.MarketScreens.SoilScreens;
using UnityEngine;
using Utility;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(SoilBuyScreen))]
	public class SoilBuyScreenText : AbstractMarketExtension
	{
		private bool hasSetText;

		private StringVariableWriter typeTextWriter;
		private StringVariableWriter priceTextWriter;
		private StringVariableWriter healthTextWriter;

		protected override void ActivateExtension(AbstractBuildingTile tile, MarketManager manager)
		{
			if (hasSetText)
			{
				return;
			}

			hasSetText = true;

			SerializableEnumDictionary<SoilType, BuyButtonData> buildingButtons =
				GetComponent<SoilBuyScreen>().GetbuyButtonData();

			foreach (KeyValuePair<SoilType, BuyButtonData> buttonData in buildingButtons)
			{
				SetText(buttonData, tile);
			}
		}

		private void SetText(KeyValuePair<SoilType, BuyButtonData> buttonData, AbstractBuildingTile tile)
		{
			SoilTypeData data = tile.GetSoilData(buttonData.Key);

			// Type
			typeTextWriter = new StringVariableWriter(buttonData.Value.Text.Type.text);

			buttonData.Value.Text.Type.text = typeTextWriter.UpdateText(buttonData.Key.ToString().InsertSpaceBeforeCapitals());

			// Price
			priceTextWriter = new StringVariableWriter(buttonData.Value.Text.Price.text);

			buttonData.Value.Text.Price.text = priceTextWriter.UpdateText(data.BuildCost);

			// Health
			healthTextWriter = new StringVariableWriter(buttonData.Value.Text.Health.text);

			buttonData.Value.Text.Health.text = healthTextWriter.UpdateText(data.MaxHealth);
		}
	}
}