using System;
using System.Collections.Generic;
using Grid.Tiles.Buildings;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework.Interfaces;
using VDFramework.Utility;

namespace UI.Market.MarketScreens
{
	public abstract class AbstractMarketBuyScreen<TBuyType, TBuyButtonData> : AbstractMarketScreen
		where TBuyType : struct, Enum
		where TBuyButtonData : struct, IBuyButtonData, IKeyValuePair<TBuyType, Button>
	{
		[SerializeField]
		private Button btnBuy = null;

#pragma warning disable 649 // is generic, therefore not recognised as serialized, but it does work
		[SerializeField]
		private SerializableEnumDictionary<TBuyType, TBuyButtonData> test;
		
		private List<TBuyButtonData> buyButtonData;
#pragma warning restore 649

		private TBuyButtonData? selectedButton;

		public List<TBuyButtonData> GetbuyButtonData()
		{
			return new List<TBuyButtonData>(buyButtonData);
		}

		protected override void SetupScreen(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupBuyButton(tile, manager);
			SetupTypeButtons(tile, manager);
		}

		protected abstract void OnSelectBuyButton(AbstractBuildingTile tile, TBuyButtonData data);

		protected virtual void BuyButtonClick(AbstractBuildingTile tile, MarketManager manager)
		{
			manager.CloseMarket();
		}

		private void SetupBuyButton(AbstractBuildingTile tile, MarketManager manager)
		{
			//TODO: block the button if we can't affort it
			SetButton(btnBuy, OnClick);

			void OnClick()
			{
				BuyButtonClick(tile, manager);
			}
		}

		private void SetupTypeButtons(AbstractBuildingTile tile, MarketManager manager)
		{
			foreach (TBuyButtonData buildingButtonDatum in buyButtonData)
			{
				// TODO: block the button if it's not unlocked yet
				SetButton(buildingButtonDatum.Value, () => Select(tile, buildingButtonDatum));
			}

			Select(tile, selectedButton ?? buyButtonData[0]);
		}

		private void Select(AbstractBuildingTile tile, TBuyButtonData datum)
		{
			if (selectedButton != null && selectedButton.Value.Equals(datum))
			{
				return;
			}

			SetTextActive(datum, true);
			btnBuy.transform.position = datum.Value.transform.position;
			
			OnSelectBuyButton(tile, datum);

			Deselect(selectedButton);
			selectedButton = datum;
		}

		private static void Deselect(TBuyButtonData? datum)
		{
			if (datum != null)
			{
				SetTextActive(datum.Value, false);
			}
		}

		protected void PopulateDictionary()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<TBuyButtonData, TBuyType, Button>(buyButtonData);
		}

		private static void SetTextActive(TBuyButtonData buttonData, bool active)
		{
			buttonData.Text.Rent.gameObject.SetActive(active);
			buttonData.Text.Health.gameObject.SetActive(active);
			buttonData.Text.Upgrades.gameObject.SetActive(active);
		}
	}
}