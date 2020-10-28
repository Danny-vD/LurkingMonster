﻿using System;
using _1._Scripts.Tests;
using Events;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace Singletons
{
	using System;

	public class MoneyManager : Singleton<MoneyManager>
	{
		public int CurrentMoney { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			CurrentMoney = UserSettings.GameData.Money;
			RegisterToOnGameQuit();
		}

		private void OnEnable()
		{
			AddListeners();
		}

		private void OnDisable()
		{
			UserSettings.GameData.Money = CurrentMoney;
			RemoveListeners();
		}

		private void AddListeners()
		{
			EventManager.Instance.AddListener<IncreaseMoneyEvent>(OnIncreaseMoney);
			EventManager.Instance.AddListener<DecreaseMoneyEvent>(OnDecreaseMoney);
			EventManager.Instance.AddListener<CollectRentEvent>(OnCollectRent);
		}

		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<IncreaseMoneyEvent>(OnIncreaseMoney);
			EventManager.Instance.RemoveListener<DecreaseMoneyEvent>(OnDecreaseMoney);
			EventManager.Instance.RemoveListener<CollectRentEvent>(OnCollectRent);
		}
		
		private void RegisterToOnGameQuit()
		{
			UserSettings.OnGameQuit += SaveCurrentMoney;
		}

		private void SaveCurrentMoney()
		{
			UserSettings.GameData.Money = CurrentMoney;
		}

		public bool PlayerHasEnoughMoney(int price)
		{
			return price <= CurrentMoney;
		}

		private void ChangeMoney(int amount)
		{
			CurrentMoney += amount;

			EventManager.Instance.RaiseEvent(new MoneyChangedEvent(CurrentMoney, amount));
		}

		private void OnIncreaseMoney(IncreaseMoneyEvent increaseMoneyEvent)
		{
			ChangeMoney(increaseMoneyEvent.Amount);
		}

		private void OnDecreaseMoney(DecreaseMoneyEvent decreaseMoneyEvent)
		{
			ChangeMoney(-decreaseMoneyEvent.Amount);
		}

		private void OnCollectRent(CollectRentEvent collectRentEvent)
		{
			ChangeMoney(collectRentEvent.Rent);
		}
	}
}