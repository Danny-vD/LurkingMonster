﻿using System.Collections.Generic;
using Enums;
using Grid.Tiles.Buildings;
using ScriptableObjects;
using Structs.Market;
using UI.Market.MarketManagers;
using UI.Market.MarketScreens.FoundationScreens;
using UnityEngine;
using Utility;
using VDFramework.Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(FoundationBuyScreen))]
	public class FoundationBuyScreenText : AbstractMarketExtension
	{
		private const string priceKey = "PRICE_DYNAMIC";
		private const string healthKey = "HEALTH_DYNAMIC";
		
		private StringVariableWriter typeTextWriter;

		protected override void ActivateExtension(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
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
			if (typeTextWriter == null)
			{
				typeTextWriter = new StringVariableWriter(buttonData.Value.Text.Type.text);
			}

			string type = LanguageUtil.GetJsonString(buttonData.Key.ToString().ToUpper());
			buttonData.Value.Text.Type.text = typeTextWriter.UpdateText(type);

			// Price
			buttonData.Value.Text.Price.text = string.Format(GetString(priceKey), data.BuildCost);

			// Health
			buttonData.Value.Text.Health.text = string.Format(GetString(healthKey), data.MaxHealth);
		}

		private static string GetString(string key)
		{
			return LanguageUtil.GetJsonString(key);
		}
	}
}