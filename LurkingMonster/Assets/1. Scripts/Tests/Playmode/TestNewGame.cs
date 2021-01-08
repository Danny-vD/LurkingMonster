using System.IO;
using Events.MoneyManagement;
using IO;
using NUnit.Framework;
using Singletons;
using UnityEngine;
using VDFramework.EventSystem;

namespace Tests.Playmode
{
    public class TestNewGame
    {
		private const int startMoney = 10000;
		
		private readonly string destination = Application.persistentDataPath + "/save.dat";
		
		private GameData gameData = new GameData(startMoney, true);
		
		[Test]
        public void TestCollectRent()
		{
			const int test = 5;
			int currentMoney = MoneyManager.Instance.CurrentMoney;
			
			EventManager.Instance.RaiseEvent(new CollectRentEvent(test));

			Assert.AreEqual(currentMoney + test, MoneyManager.Instance.CurrentMoney);
		}
		
		[Test]
		public void TestDecreaseMoney()
		{
			const int test = 5;
			int currentMoney = MoneyManager.Instance.CurrentMoney;
			
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(test));
			Assert.AreEqual(currentMoney - test, MoneyManager.Instance.CurrentMoney);
		}
		
		[Test]
		public void TestIncreaseMoney()
		{
			const int test = 5;
			int currentMoney = MoneyManager.Instance.CurrentMoney;
			
			EventManager.Instance.RaiseEvent(new IncreaseMoneyEvent(test));
			Assert.AreEqual(currentMoney + test, MoneyManager.Instance.CurrentMoney);
		}
		
		[Test]
		public void TestStartMoney()
		{
			if (!File.Exists(destination))
			{
				Assert.AreEqual(startMoney, UserSettings.GameData.Money);
			}
			else
			{
				Assert.Pass();
			}
		}
	}
}
