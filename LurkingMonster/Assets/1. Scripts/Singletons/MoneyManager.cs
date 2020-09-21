using Events;
using VDFramework.EventSystem;
using VDFramework.Singleton;

namespace Singletons
{
	public class MoneyManager : Singleton<MoneyManager>
	{
		public int CurrentMoney { get; private set; } = 10000;

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

		public bool PlayerHasEnoughMoney(int money)
		{
			return money <= CurrentMoney;
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