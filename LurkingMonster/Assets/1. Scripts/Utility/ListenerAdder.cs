using UI.Market;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Utility
{
	public class ListenerAdder : BetterMonoBehaviour
	{
		[SerializeField]
		private MarketManager listener;

		private void Start()
		{
			listener.AddListeners();
		}

		private void OnDestroy()
		{
			if (EventManager.IsInitialized)
			{
				listener.RemoveListeners();
			}
		}

		#region IntendedCode

		/*
		// Can't do this because [SerializeReference] is broken in Unity 2020.1.12f
		
		[SerializeReference]
		private readonly List<IListener> eventListeners = new List<IListener>();

		private void Awake()
		{
			foreach (IListener eventListener in eventListeners)
			{
				eventListener.AddListeners();
			}
		}
		
		private void OnDestroy()
		{
			foreach (IListener eventListener in eventListeners)
			{
				eventListener.RemoveListeners();
			}
		}
		*/

		#endregion
	}
}