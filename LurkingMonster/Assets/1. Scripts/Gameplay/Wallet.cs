using Events;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class Wallet : BetterMonoBehaviour
	{
		private Text txtWallet;
		private float currentMoney;

		private void Awake()
		{
			txtWallet = GetComponent<Text>();
			AddRentListener();
		}

		private void AddRentListener()
		{
			EventManager.Instance.AddListener<CollectRentEvent>(OnCollectRent);
		}

		private void OnCollectRent(CollectRentEvent collectRentEvent)
		{
			IncreaseMoney(collectRentEvent.Rent);
		}

		private void IncreaseMoney(float increaseAmount)
		{
			currentMoney   += increaseAmount;
			txtWallet.text =  "Money : " + currentMoney;
		}
	}
}