using Events.MoneyManagement;
using Events.SoilSamplesManagement;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace Singletons
{
	public class MoneyManager : Singleton<MoneyManager>
	{
		public int CurrentMoney { get; private set; }
		
		public int CurrentSoilSamples { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			CurrentMoney       = UserSettings.GameData.Money;
			CurrentSoilSamples = UserSettings.GameData.SoilSamples;
		}

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
			UserSettings.OnGameQuit += SaveCurrentCurrencies;

			EventManager.Instance.AddListener<IncreaseMoneyEvent>(OnIncreaseMoney);
			EventManager.Instance.AddListener<DecreaseMoneyEvent>(OnDecreaseMoney);
			EventManager.Instance.AddListener<CollectRentEvent>(OnCollectRent);
			EventManager.Instance.AddListener<IncreaseSoilSamplesEvent>(OnIncreaseSoilSamples, 100);
			EventManager.Instance.AddListener<DecreaseSoilSamplesEvent>(OnDecreaseSoilSamples, 100);
		}

		private void RemoveListeners()
		{
			UserSettings.OnGameQuit -= SaveCurrentCurrencies;

			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<IncreaseMoneyEvent>(OnIncreaseMoney);
			EventManager.Instance.RemoveListener<DecreaseMoneyEvent>(OnDecreaseMoney);
			EventManager.Instance.RemoveListener<CollectRentEvent>(OnCollectRent);
		}

		private void SaveCurrentCurrencies()
		{
			UserSettings.GameData.Money       = CurrentMoney;
			UserSettings.GameData.SoilSamples = CurrentSoilSamples;
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

		private void OnIncreaseSoilSamples(IncreaseSoilSamplesEvent @event)
		{
			CurrentSoilSamples += @event.Amount;
		}

		private void OnDecreaseSoilSamples(DecreaseSoilSamplesEvent @event)
		{
			CurrentSoilSamples -= @event.Amount;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Debug method to add money to the player
		/// </summary>
		[ContextMenu("Add 10.000")]
		private void Add10()
		{
			ChangeMoney(10000);
		}
		
		[ContextMenu("Add 100.000")]
		private void Add100()
		{
			ChangeMoney(100000);
		}
		
		[ContextMenu("Add 1.000.000")]
		private void Add1000()
		{
			ChangeMoney(1000000);
		}
#endif
	}
}