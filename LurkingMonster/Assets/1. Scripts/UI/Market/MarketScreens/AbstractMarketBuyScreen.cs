using System;
using System.Collections.Generic;
using System.Linq;
using Grid.Tiles.Buildings;
using Structs.Market;
using Structs.Utility;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Market.MarketScreens
{
	public abstract class AbstractMarketBuyScreen<TBuyType> : AbstractMarketScreen
		where TBuyType : struct, Enum
	{
		[SerializeField]
		private Button btnBuy = null;

		[SerializeField]
		private SerializableEnumDictionary<TBuyType, BuyButtonData> buttonDataPerBuyType;

		private SerializableKeyValuePair<TBuyType, BuyButtonData>? selectedButtonDatum;

		protected override void SetupScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			selectedButtonDatum = null;
			SetupTypeButtons(tile, manager);
		}

		public SerializableEnumDictionary<TBuyType, BuyButtonData> GetbuyButtonData()
		{
			return buttonDataPerBuyType;
		}
		
		protected abstract void OnSelectBuyButton(AbstractBuildingTile tile, TBuyType buyType);

		//TODO: use the reward manager for the overrides
		protected abstract TBuyType[] GetUnlockedTypes();

		protected abstract int GetPrice(AbstractBuildingTile tile);

		protected virtual void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			manager.CloseMarket();
		}

		private void SetupBuyButton(AbstractBuildingTile tile, MarketManager manager)
		{
			int price = GetPrice(tile);

			if (!CanAffort(price))
			{
				BlockButton(btnBuy, true);
				return;
			}

			BlockButton(btnBuy, false);

			SetButton(btnBuy, OnClick);

			void OnClick()
			{
				ReduceMoney(price);
				BuyButtonClick(tile, manager);
			}
		}

		private void SetupTypeButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			TBuyType[] unlocked = GetUnlockedTypes();

			foreach (KeyValuePair<TBuyType, BuyButtonData> pair in buttonDataPerBuyType)
			{
				Button button = pair.Value.Button;

				if (!unlocked.Contains(pair.Key))
				{
					BlockButton(button, true);
					continue;
				}

				if (selectedButtonDatum == null)
				{
					Select(tile, pair, manager);
				}

				BlockButton(button, false);
				SetButton(button, () => Select(tile, pair, manager));
			}
		}

		private void Select(AbstractBuildingTile tile, SerializableKeyValuePair<TBuyType, BuyButtonData> pair, MarketManager manager)
		{
			// if we select the selected button, don't do anything
			if (selectedButtonDatum != null && selectedButtonDatum.Value.Equals(pair))
			{
				return;
			}

			BuyButtonData buyButtondatum = pair.Value;
			SetTextActive(buyButtondatum, true);
			btnBuy.transform.position = buyButtondatum.Button.transform.position;

			OnSelectBuyButton(tile, pair.Key);

			Deselect(selectedButtonDatum);
			selectedButtonDatum = pair;

			SetupBuyButton(tile, manager);
		}

		private static void Deselect(SerializableKeyValuePair<TBuyType, BuyButtonData>? datum)
		{
			if (datum != null)
			{
				SetTextActive(datum.Value.Value, false);
			}
		}

		private static void SetTextActive(BuyButtonData buttonData, bool active)
		{
			if (buttonData.Text.Rent)
			{
				buttonData.Text.Rent.gameObject.SetActive(active);
			}

			if (buttonData.Text.Health)
			{
				buttonData.Text.Health.gameObject.SetActive(active);
			}

			if (buttonData.Text.Upgrades)
			{
				buttonData.Text.Upgrades.gameObject.SetActive(active);
			}
		}
	}
}