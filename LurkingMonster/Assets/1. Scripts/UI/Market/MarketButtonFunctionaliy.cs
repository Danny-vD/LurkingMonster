using System;
using Events;
using Gameplay.Buildings;
using Grid.Tiles;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.EventSystem;

namespace UI.Market
{
	public class MarketButtonFunctionaliy : MonoBehaviour
	{
		[SerializeField]
		private Button buyButton = null;
		
		[SerializeField]
		private Button someOtherButton = null;
		
		[SerializeField]
		private Button theOtherButton = null;

		private Text buyText;

		private void Awake()
		{
			buyText = buyButton.GetComponentInChildren<Text>();
		}

		private void OnEnable()
		{
			AddListeners();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}
		
		private void AddListeners()
		{
			EventManager.Instance.AddListener<OpenMarketEvent>(OnMarketOpened);
		}
		
		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
    
			EventManager.Instance.RemoveListener<OpenMarketEvent>(OnMarketOpened);
		}

		private void OnMarketOpened(OpenMarketEvent openMarketEvent)
		{
			SetBuyButton(openMarketEvent.BuildingTile);
		}

		private void SetBuyButton(AbstractBuildingTile buildingTile)
		{
			buyButton.onClick.RemoveAllListeners();
			
			if (buildingTile.Building)
			{
				if (buildingTile.Building.IsMaxTier)
				{
					SetBuyText($"MAX UPGRADED");
					return;
				}
				
				SetBuyText($"Upgrade [{buildingTile.Building.UpgradeCost}]");
				buyButton.onClick.AddListener(buildingTile.Building.GetComponent<BuildingUpgrade>().Upgrade);
				return; 
			}
			
			SetBuyText($"Buy House [{buildingTile.GetBuildingPrice()}]");
			buyButton.onClick.AddListener(buildingTile.SpawnBuilding);
		}

		private void SetBuyText(string text)
		{
			buyText.text = text;
		}
	}
}
