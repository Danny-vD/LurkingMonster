using System;
using Events.Temporary;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Temporary
{
	public class WinScreenEnabler : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject winScreen = null;
		
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
			EventManager.Instance.AddListener<WinGameEvent>(OnWinGame);
		}
		
		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
    
			EventManager.Instance.RemoveListener<WinGameEvent>(OnWinGame);
		}

		private void OnWinGame()
		{
			if (!winScreen)
			{
				throw new Exception("You forgot to assign the Win Gameobject");
			}
			
			winScreen.SetActive(true);
		}
	}
}