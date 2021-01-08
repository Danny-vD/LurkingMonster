using Events.MoneyManagement;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace Singletons
{
	public class MoneyManager : Singleton<MoneyManager>
	{
		public int CurrentMoney { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			CurrentMoney = UserSettings.GameData.Money;
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
			UserSettings.OnGameQuit += SaveCurrentMoney;

			EventManager.Instance.AddListener<IncreaseMoneyEvent>(OnIncreaseMoney);
			EventManager.Instance.AddListener<DecreaseMoneyEvent>(OnDecreaseMoney);
			EventManager.Instance.AddListener<CollectRentEvent>(OnCollectRent);
		}

		private void RemoveListeners()
		{
			UserSettings.OnGameQuit -= SaveCurrentMoney;

			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<IncreaseMoneyEvent>(OnIncreaseMoney);
			EventManager.Instance.RemoveListener<DecreaseMoneyEvent>(OnDecreaseMoney);
			EventManager.Instance.RemoveListener<CollectRentEvent>(OnCollectRent);
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