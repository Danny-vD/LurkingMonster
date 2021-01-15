using UI.Market.MarketScreens;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Market.MarketManagers
{
	public abstract class AbstractMarketManager : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject[] enableOnExit = new GameObject[0];

		[SerializeField]
		private Button btnExit;

		[SerializeField]
		private Structs.Market.MarketScreens screens;

		public Structs.Market.MarketScreens Screens => screens;

		public void OpenMarket()
		{
			CachedGameObject.SetActive(true);
			SetupReturnButtons();

			PutScreenInFocus(screens.MainScreen);
		}

		public void CloseMarket()
		{
			gameObject.SetActive(false);

			foreach (GameObject @object in enableOnExit)
			{
				@object.SetActive(true);
			}
		}
		
		public void PutScreenInFocus(AbstractMarketScreen screen)
		{
			screens.Screens.ForEach(Hide);
			Show(screen);
			OnScreenFocus(screen);
		}

		protected virtual void OnScreenFocus(AbstractMarketScreen screen)
		{
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