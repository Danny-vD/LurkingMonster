using System;
using Events.MoneyManagement;
using Grid.Tiles.Buildings;
using Singletons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.UnityExtensions;

namespace UI.Market.MarketScreens
{
	public abstract class AbstractMarketScreen : BetterMonoBehaviour
	{
		[SerializeField]
		private Button returnButton = null;

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
				return;
			}

			button.onClick.RemoveAllListeners();

			foreach (UnityAction listener in onClickListeners)
			{
				button.onClick.AddListener(listener);
			}
		}

		protected static void BlockButton(Button button, bool block)
		{
			if (block)
			{
				button.onClick.RemoveAllListeners();
			}

			button.EnsureComponent<LockEnabler>().SetLocked(block);
		}

		protected static bool CanAffort(int price)
		{
			return MoneyManager.Instance.PlayerHasEnoughMoney(price);
		}

		protected static void ReduceMoney(int price)
		{
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(price));
		}
	}
}