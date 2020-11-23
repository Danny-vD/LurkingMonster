using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VDFramework;

namespace UI.Market.MarketScreens
{
	public abstract class AbstractMarketScreen : BetterMonoBehaviour
	{
		[SerializeField]
		private Button returnButton = null;

		public void Hide()
		{
			CachedGameObject.SetActive(false);
		}

		public void Show()
		{
			CachedGameObject.SetActive(true);
		}
		
		public void SetReturnButton(UnityAction action)
		{
			if (!returnButton)
			{
				return;
			}
			
			returnButton.onClick.RemoveAllListeners();
			returnButton.onClick.AddListener(action);
		}

		public abstract void SetUI(AbstractBuildingTile tile, MarketManager manager);

		protected void SetButton(Button button, params UnityAction[] onClickListeners)
		{
			if (!button)
			{
				Debug.LogWarning("A button is not set in the inspector!", this);
			}
			
			button.onClick.RemoveAllListeners();
			
			foreach (UnityAction listener in onClickListeners)
			{
				button.onClick.AddListener(listener);
			}
		} 
	}
}