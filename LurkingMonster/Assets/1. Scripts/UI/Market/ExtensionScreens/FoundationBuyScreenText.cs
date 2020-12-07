using System.Collections.Generic;
using Enums;
using Grid.Tiles.Buildings;
using ScriptableObjects;
using Structs.Market;
using UI.Market.MarketScreens.FoundationScreens;
using UnityEngine;
using Utility;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(FoundationBuyScreen))]
	public class FoundationBuyScreenText : AbstractMarketExtension
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

			SerializableEnumDictionary<FoundationType, BuyButtonData> buildingButtons =
				GetComponent<FoundationBuyScreen>().GetbuyButtonData();

			foreach (KeyValuePair<FoundationType, BuyButtonData> buttonData in buildingButtons)
			{
				SetText(buttonData, tile);
			}
		}

		private void SetText(KeyValuePair<FoundationType, BuyButtonData> buttonData, AbstractBuildingTile tile)
		{
			FoundationTypeData data = tile.GetFoundationData(buttonData.Key);

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