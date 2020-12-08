using Events;
using Grid.Tiles.Buildings;
using Interfaces;
using UI.Market.MarketScreens;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Market
{
	public class MarketManager : BetterMonoBehaviour, IListener
	{
		[SerializeField]
		private Button btnExit;
		
		[SerializeField]
		private Structs.Market.MarketScreens screens;

		public Structs.Market.MarketScreens Screens => screens;

		private AbstractBuildingTile tile;

		private void Start()
		{
			SetupReturnButtons();
		}

		public void AddListeners()
		{
			EventManager.Instance.AddListener<OpenMarketEvent>(OpenMarket, 1);
		}

		public void PutScreenInFocus(AbstractMarketScreen screen)
		{
			screens.Screens.ForEach(Hide);
			Show(screen);
			SetExitButtonInteractable(true);
			screen.SetUI(tile, this);
		}

		public void CloseMarket()
		{
			btnExit.onClick.Invoke();
		}

		public void SetExitButtonInteractable(bool interactable)
		{
			btnExit.interactable = interactable;
		}
		
		private void SetupReturnButtons()
		{
			// Setup every back button to go back to the main screen 
			screens.Screens.ForEach(screen => screen.SetReturnButton(() => PutScreenInFocus(screens.MainScreen)));

			// Set the mainscreen back button to exit market
			screens.MainScreen.SetReturnButton(() => gameObject.SetActive(false));
		}
		
		private void OpenMarket(OpenMarketEvent openMarketEvent)
		{
			CachedGameObject.SetActive(true);
			
			tile = openMarketEvent.BuildingTile;
			
			if (!tile)
			{
				tile = openMarketEvent.Building.GetComponentInParent<AbstractBuildingTile>();
			}

			PutScreenInFocus(screens.MainScreen);
		}
		
		private static void Show(AbstractMarketScreen screen)
		{
			screen.Show();
		}

		private static void Hide(AbstractMarketScreen screen)
		{
			screen.Hide();
		}
	}
}