using System;
using Events.MoneyManagement;
using Grid.Tiles.Buildings;
using Singletons;
using UI.Market.MarketManagers;
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

		public event Action<AbstractBuildingTile, AbstractMarketManager> Extensions;

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
			// Not using SetButton because in this case it's perfectly valid to not have a return button
			if (!returnButton)
			{
				return;
			}

			returnButton.onClick.RemoveAllListeners();
			returnButton.onClick.AddListener(action);
		}

		public void SetUI(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			SetupScreen(tile, manager);
			ActivateExtensions(tile, manager);
		}

		protected abstract void SetupScreen(AbstractBuildingTile tile, AbstractMarketManager manager);

		private void ActivateExtensions(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			Extensions?.Invoke(tile, manager);
		}

		protected void SetButton(Button button, UnityAction onClickListener)
		{
			if (!button)
			{
				Debug.LogWarning("A button is not set in the inspector!", this);
				return;
			}

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(onClickListener);
		}

		protected static void BlockButton(Button button, bool block)
		{
			if (block)
			{
				button.onClick.RemoveAllListeners();
			}

			button.EnsureComponent<LockEnabler>().SetLocked(block);
		}

		protected virtual bool CanAffort(int price)
		{
			return MoneyManager.Instance.PlayerHasEnoughMoney(price);
		}

		protected virtual void ReduceMoney(int price)
		{
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(price));
		}
	}
}