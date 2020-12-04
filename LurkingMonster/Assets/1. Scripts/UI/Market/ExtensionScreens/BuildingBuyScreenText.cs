using System.Collections.Generic;
using Enums;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using Structs.Market;
using UI.Market.MarketScreens.BuildingScreens;
using UnityEngine;
using Utility;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(BuildingBuyScreen))]
	public class BuildingBuyScreenText : AbstractMarketExtension
	{
		private bool hasSetText;

		private StringVariableWriter typeTextWriter;
		private StringVariableWriter priceTextWriter;
		private StringVariableWriter healthTextWriter;
		private StringVariableWriter rentTextWriter;
		private StringVariableWriter upgradeTextWriter;

		protected override void ActivateExtension(AbstractBuildingTile tile, MarketManager manager)
		{
			if (hasSetText)
			{
				return;
			}

			hasSetText = true;

			SerializableEnumDictionary<BuildingType, BuyButtonData> buildingButtons = GetComponent<BuildingBuyScreen>().GetbuyButtonData();

			foreach (KeyValuePair<BuildingType, BuyButtonData> buttonData in buildingButtons)
			{
				SetText(buttonData, tile);
			}
		}

		private void SetText(KeyValuePair<BuildingType, BuyButtonData> buttonData, AbstractBuildingTile tile)
		{
			BuildingData[] data = tile.GetBuildingData(buttonData.Key);

			// Type
			typeTextWriter = new StringVariableWriter(buttonData.Value.Text.Type.text);

			buttonData.Value.Text.Type.text = typeTextWriter.UpdateText(buttonData.Key.ToString().InsertSpaceBeforeCapitals());

			// Price
			priceTextWriter = new StringVariableWriter(buttonData.Value.Text.Price.text);

			buttonData.Value.Text.Price.text = priceTextWriter.UpdateText(data[0].Price);

			// Health
			healthTextWriter = new StringVariableWriter(buttonData.Value.Text.Health.text);

			buttonData.Value.Text.Health.text = healthTextWriter.UpdateText(data[0].MaxHealth);

			// Rent
			rentTextWriter = new StringVariableWriter(buttonData.Value.Text.Rent.text);

			buttonData.Value.Text.Rent.text = rentTextWriter.UpdateText(data[0].Rent);

			// Upgrades
			upgradeTextWriter = new StringVariableWriter(buttonData.Value.Text.Upgrades.text);

			buttonData.Value.Text.Upgrades.text = upgradeTextWriter.UpdateText(data.Length);
		}
	}
}