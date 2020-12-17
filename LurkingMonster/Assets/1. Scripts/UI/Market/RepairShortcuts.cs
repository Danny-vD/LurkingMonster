using System;
using System.Collections.Generic;
using Events;
using Events.BuildingEvents;
using Events.MoneyManagement;
using Gameplay;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.UnityExtensions;

namespace UI.Market
{
	public class RepairShortcuts : BetterMonoBehaviour
	{
		private enum ShortCutButton
		{
			Market,
			Building,
			Foundation,
			Soil,
		}

		[Serializable]
		private struct ButtonData
		{
			public Button Button;
			public TextMeshProUGUI PriceText;
			public Bar healthBar;
		}

		[SerializeField]
		private SerializableEnumDictionary<ShortCutButton, ButtonData> buttons;

		private GameObject child;
		private AbstractBuildingTile selectedTile;
		private BuildingHealth buildingHealth;

		private void Start()
		{
			EventManager.Instance.AddListener<SelectedBuildingEvent>(OnSelectedBuilding);
			EventManager.Instance.AddListener<OpenMarketEvent>(OnOpenMarket);

			child = CachedTransform.GetChild(0).gameObject;
		}

		private void Update()
		{
			if (!child.activeSelf)
			{
				return;
			}

			if (selectedTile && selectedTile.HasDebris)
			{
				selectedTile   = null;
				buildingHealth = null;
				SetActive(false);
			}

			SetBars();
		}

		private void OnSelectedBuilding(SelectedBuildingEvent selectedBuildingEvent)
		{
			AbstractBuildingTile tile = selectedBuildingEvent.tile;

			if (tile == null || tile.HasDebris)
			{
				SetActive(false);
				return;
			}

			SetActive(true);
			SetButtons(tile);
		}

		private void SetActive(bool active)
		{
			child.SetActive(active);
		}

		private void SetBars()
		{
			foreach (KeyValuePair<ShortCutButton, ButtonData> pair in buttons)
			{
				Bar healthBar = pair.Value.healthBar;

				switch (pair.Key)
				{
					case ShortCutButton.Market:
						continue;
					case ShortCutButton.Building:
						buildingHealth.SetBuildingHealthBar(healthBar);
						break;
					case ShortCutButton.Foundation:
						buildingHealth.SetFoundationHealthBar(healthBar);
						break;
					case ShortCutButton.Soil:
						buildingHealth.SetSoilHealthBar(healthBar);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private void SetButtons(AbstractBuildingTile tile)
		{
			selectedTile   = tile;
			buildingHealth = tile.Building.GetComponent<BuildingHealth>();

			foreach (KeyValuePair<ShortCutButton, ButtonData> pair in buttons)
			{
				switch (pair.Key)
				{
					case ShortCutButton.Market:
						SetButton(pair.Value.Button, OpenMarket);
						break;
					case ShortCutButton.Building:
						SetBuildingButton(pair.Value);
						break;
					case ShortCutButton.Foundation:
						SetFoundationButton(pair.Value);
						break;
					case ShortCutButton.Soil:
						SetSoilButton(pair.Value);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private static void SetButton(Button button, UnityAction action)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(action);
		}

		private void SetBuildingButton(ButtonData buttonData)
		{
			int price = selectedTile.Building.Data.RepairCost;
			Button button = buttonData.Button;

			buttonData.PriceText.text = price.ToString();
			buildingHealth.SetBuildingHealthBar(buttonData.healthBar);

			if (!CanAffort(price))
			{
				BlockButton(button, true);
				return;
			}

			BlockButton(button, false);
			SetButton(button, OnClick);

			void OnClick()
			{
				ReduceMoney(price);
				buildingHealth.ResetBuildingHealth();
			}
		}

		private void SetFoundationButton(ButtonData buttonData)
		{
			int price = selectedTile.GetCurrentFoundationData().RepairCost;
			Button button = buttonData.Button;

			buttonData.PriceText.text = price.ToString();
			buildingHealth.SetFoundationHealthBar(buttonData.healthBar);

			if (!CanAffort(price))
			{
				BlockButton(button, true);
				return;
			}

			BlockButton(button, false);
			SetButton(button, OnClick);

			void OnClick()
			{
				ReduceMoney(price);
				buildingHealth.ResetFoundationHealth();
			}
		}

		private void SetSoilButton(ButtonData buttonData)
		{
			int price = selectedTile.GetCurrentSoilData().RepairCost;

			Button button = buttonData.Button;

			buttonData.PriceText.text = price.ToString();
			buildingHealth.SetFoundationHealthBar(buttonData.healthBar);

			if (!CanAffort(price))
			{
				BlockButton(button, true);
				return;
			}

			BlockButton(button, false);
			SetButton(button, OnClick);

			void OnClick()
			{
				ReduceMoney(price);
				buildingHealth.ResetSoilHealth();
			}
		}

		private void OpenMarket()
		{
			EventManager.Instance.RaiseEvent(new OpenMarketEvent(selectedTile));
		}

		private static void BlockButton(Button button, bool block)
		{
			if (block)
			{
				button.onClick.RemoveAllListeners();
			}

			button.EnsureComponent<LockEnabler>().SetLocked(block);
		}

		private static bool CanAffort(int price)
		{
			return MoneyManager.Instance.PlayerHasEnoughMoney(price);
		}

		private static void ReduceMoney(int price)
		{
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(price));
		}

		private void OnOpenMarket()
		{
			SetActive(false);
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<SelectedBuildingEvent>(OnSelectedBuilding);
		}
	}
}