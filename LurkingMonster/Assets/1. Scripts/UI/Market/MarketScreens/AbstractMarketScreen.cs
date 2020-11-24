using System;
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
		
		// Have so called 'extension classes' automatically add themselves, Abstract this logic
		public event Action<AbstractBuildingTile, MarketManager> Extensions;

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

		public void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			SetupScreen(tile, manager);
			ActivateExtensions(tile, manager);
		}

		protected abstract void SetupScreen(AbstractBuildingTile tile, MarketManager manager);
		
		private void ActivateExtensions(AbstractBuildingTile tile, MarketManager manager)
		{
			Extensions?.Invoke(tile, manager);
		}

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

		protected virtual void OnActivate(AbstractBuildingTile arg1, MarketManager arg2)
		{
			Extensions?.Invoke(arg1, arg2);
		}
	}
}