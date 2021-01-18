using System;
using Interfaces;
using VDFramework.EventSystem;

namespace UI.Market.MarketManagers
{
	public abstract class EventMarketManager<T> : AbstractMarketManager, IListener
		where T : VDEvent
	{
		public void AddListeners()
		{
			EventManager.Instance.AddListener<T>(EnableMarket, 1);
		}
		
		public void RemoveListeners()
		{
			EventManager.Instance.RemoveListener<T>(EnableMarket);
		}

		protected abstract void SetupMarket(T @event);

		private void EnableMarket(T @event)
		{
			SetupMarket(@event);
			
			OpenMarket();
		}
	}
}