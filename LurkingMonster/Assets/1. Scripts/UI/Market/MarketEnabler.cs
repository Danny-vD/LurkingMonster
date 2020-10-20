using System.Collections.Generic;
using Events;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Market
{
	public class MarketEnabler : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject market = null;

		[SerializeField]
		private List<GameObject> objectsToDisable = new List<GameObject>();
		
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
			EventManager.Instance.AddListener<OpenMarketEvent>(EnableMarket);
		}
		
		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
    
			EventManager.Instance.RemoveListener<OpenMarketEvent>(EnableMarket);
		}

		private void EnableMarket()
		{
			market.SetActive(true);
			
			objectsToDisable.ForEach(@object => @object.gameObject.SetActive(false));
		}
	}
}
