using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
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
		private Button buyFoundationButton = null;

		[SerializeField]
		private Button destroyButton = null;

		private Text buyText;
		private Text buyFoundationText;
		private Text destroyText;

		private void Awake()
		{
			buyText           = buyButton.GetComponentInChildren<Text>();
			buyFoundationText = buyFoundationButton.GetComponentInChildren<Text>();
			destroyText       = destroyButton.GetComponentInChildren<Text>();
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
			AbstractBuildingTile buildingTile = openMarketEvent.BuildingTile;
			
			if (!buildingTile)
			{
				buildingTile = openMarketEvent.Building.GetComponentInParent<AbstractBuildingTile>();
			}
			
			SetBuyButton(buildingTile);
			SetBuyFoundationButton(buildingTile);
			SetDestroyButton(buildingTile);
		}

		private void SetBuyButton(AbstractBuildingTile buildingTile)
		{
			buyButton.onClick.RemoveAllListeners();

			if (buildingTile.HasDebris)
			{
				SetBuyText("PLOT OBSTRUCTED!");
				return;
			}
			
			if (!buildingTile.HasFoundation)
			{
				SetBuyText("NO FOUNDATION!");
				return;
			}

			if (buildingTile.Building)
			{
				if (buildingTile.Building.IsMaxTier)
				{
					SetBuyText("MAX UPGRADED"); //LanguageUtil.GetJsonString("MAXUPGRADE")
					return;
				}

				SetBuyText($"Upgrade [{buildingTile.Building.UpgradeCost}]");
				buyButton.onClick.AddListener(() => buildingTile.Building.GetComponent<BuildingUpgrade>().Upgrade(true));
				return;
			}

			SetBuyText($"Buy House [{buildingTile.GetBuildingPrice()}]");
			buyButton.onClick.AddListener(buildingTile.SpawnBuilding);
		}

		private void SetBuyFoundationButton(AbstractBuildingTile buildingTile)
		{
			buyFoundationButton.onClick.RemoveAllListeners();

			if (buildingTile.HasDebris)
			{
				SetBuyFoundationText("PLOT OBSTRUCTED!");
				return;
			}
			
			if (buildingTile.HasFoundation)
			{
				SetBuyFoundationText("FOUNDATION ALREADY BUILD");
				return;
			}
			
			SetBuyFoundationText($"Buy foundation [{buildingTile.GetFoundationData(buildingTile.GetFoundationType()).BuildCost}]");
			buyFoundationButton.onClick.AddListener(buildingTile.SpawnFoundation);
		}

		private void SetDestroyButton(AbstractBuildingTile buildingTile)
		{
			destroyButton.onClick.RemoveAllListeners();

			if (buildingTile.HasDebris)
			{
				SetDestroyText($"Remove debris [{buildingTile.DebrisRemovalCost}]");
				destroyButton.onClick.AddListener(() => buildingTile.RemoveDebris(true));
				return;
			}
			
			if (buildingTile.Building)
			{
				SetDestroyText($"Remove [{buildingTile.Building.GlobalData.DestructionCost}]");
				destroyButton.onClick.AddListener(() => buildingTile.Building.RemoveBuilding(true));
				return;
			}

			if (buildingTile.HasFoundation)
			{
				SetDestroyText($"Remove foundation [{buildingTile.GetFoundationData(buildingTile.GetFoundationType()).DestroyCost}]");
				destroyButton.onClick.AddListener(() => buildingTile.RemoveFoundation(true));
				return;
			}

			SetDestroyText("Nothing to remove");
		}

		private void SetBuyText(string text)
		{
			buyText.text = text;
		}

		private void SetBuyFoundationText(string text)
		{
			buyFoundationText.text = text;
		}

		private void SetDestroyText(string text)
		{
			destroyText.text = text;
		}
	}
}