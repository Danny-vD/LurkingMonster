using Enums;
using Events;
using Singletons;
using UnityEngine.UI;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class MoneyText : BetterMonoBehaviour
	{
		private Text moneyText;

		private void Start()
		{
			moneyText = GetComponent<Text>();
			AddMoneyListener();
			
			SetText(MoneyManager.Instance.CurrentMoney);
		}

		private void AddMoneyListener()
		{
			EventManager.Instance.AddListener<MoneyChangedEvent>(OnMoneyChanged);
		}

		private void SetText(int currentMoney)
		{
			moneyText.text =  LanguageUtil.GetJsonString("MONEY") + currentMoney;
		}
		
		private void OnMoneyChanged(MoneyChangedEvent moneyChangedEvent)
		{
			SetText(moneyChangedEvent.CurrentMoney);
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<MoneyChangedEvent>(OnMoneyChanged);
		}
	}
}