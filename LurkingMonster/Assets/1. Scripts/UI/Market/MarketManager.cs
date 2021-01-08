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
		private GameObject[] enableOnExit = new GameObject[0];
		
		[SerializeField]
		private Button btnExit;
		
		[SerializeField]
		private Structs.Market.MarketScreens screens;

		public Structs.Market.MarketScreens Screens => screens;

		private AbstractBuildingTile tile;

		public void AddListeners()
		{
			EventManager.Instance.AddListener<OpenMarketEvent>(OpenMarket, 1);
		}

		public void RemoveListeners()
		{
			EventManager.Instance.RemoveListener<OpenMarketEvent>(OpenMarket);
		}

		public void PutScreenInFocus(AbstractMarketScreen screen)
		{
			screens.Screens.ForEach(Hide);
			Show(screen);
			screen.SetUI(tile, this);
		}

		public void CloseMarket()
		{
			gameObject.SetActive(false);

			foreach (GameObject @object in enableOnExit)
			{
				@object.SetActive(true);
			}
		}

		private void SetupReturnButtons()
		{
			// Setup every back button to go back to the main screen 
			screens.Screens.ForEach(screen => screen.SetReturnButton(() => PutScreenInFocus(screens.MainScreen)));

			// Set the mainscreen back button to exit market
			screens.MainScreen.SetReturnButton(CloseMarket);
			
			// Setup the market exit button
			btnExit.onClick.RemoveAllListeners();
			btnExit.onClick.AddListener(CloseMarket);
		}
		
		private void OpenMarket(OpenMarketEvent openMarketEvent)
		{
			CachedGameObject.SetActive(true);
			SetupReturnButtons();
			
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