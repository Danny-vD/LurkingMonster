﻿using System.Collections.Generic;
using Enums;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using Structs.Market;
using UI.Market.MarketScreens.BuildingScreens;
using UnityEngine;
using Utility;
using VDFramework.Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(BuildingBuyScreen))]
	public class BuildingBuyScreenText : AbstractMarketExtension
	{
		private bool hasSetText;

		private StringVariableWriter typeTextWriter;

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
			if (typeTextWriter == null)
			{
				typeTextWriter = new StringVariableWriter(buttonData.Value.Text.Type.text);
			}

			string type = LanguageUtil.GetJsonString(buttonData.Key.ToString().ToUpper());
			buttonData.Value.Text.Type.text = typeTextWriter.UpdateText(type);

			// Price
			buttonData.Value.Text.Price.text = string.Format(buttonData.Value.Text.Price.text, data[0].Price);

			// Health
			buttonData.Value.Text.Health.text = string.Format(buttonData.Value.Text.Health.text, data[0].MaxHealth);

			// Rent
			//TODO: hook up with buildingRent somehow?
			const float rentcollectionsPerHour = (60.0f / 18.0f) * 60.0f;
			buttonData.Value.Text.Rent.text = string.Format(buttonData.Value.Text.Rent.text, data[0].Rent * rentcollectionsPerHour);

			// Upgrades
			buttonData.Value.Text.Upgrades.text = string.Format(buttonData.Value.Text.Upgrades.text, data.Length);
		}
	}
}