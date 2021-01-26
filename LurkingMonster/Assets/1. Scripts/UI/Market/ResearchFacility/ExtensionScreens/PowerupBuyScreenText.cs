using System.Collections.Generic;
using Enums;
using Gameplay;
using Grid.Tiles.Buildings;
using Singletons;
using Structs.Market;
using UI.Market.ExtensionScreens;
using UI.Market.MarketManagers;
using UnityEngine;
using Utility;
using VDFramework.Utility;

namespace UI.Market.ResearchFacility.ExtensionScreens
{
	[RequireComponent(typeof(PowerupBuyScreen))]
	public class PowerupBuyScreenText : AbstractMarketExtension
	{
		private const string priceKey = "PRICE_DYNAMIC";
		
		private StringVariableWriter typeTextWriter;

		protected override void ActivateExtension(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			SerializableEnumDictionary<PowerUpType, BuyButtonData> buildingButtons = GetComponent<PowerupBuyScreen>().GetbuyButtonData();

			foreach (KeyValuePair<PowerUpType, BuyButtonData> buttonData in buildingButtons)
			{
				SetText(buttonData, tile);
			}
		}

		private static void SetText(KeyValuePair<PowerUpType, BuyButtonData> buttonData, AbstractBuildingTile tile)
		{
			PowerUpType powerUpType = buttonData.Key;

			PowerUp powerUp = PowerUpManager.Instance.GetPowerUp(powerUpType);

			// Price
			string price = $"{powerUp.Price:N0}";
			buttonData.Value.Text.Price.text = string.Format(GetString(priceKey), price);
		}

		private static string GetString(string key)
		{
			return LanguageUtil.GetJsonString(key);
		}
	}
}