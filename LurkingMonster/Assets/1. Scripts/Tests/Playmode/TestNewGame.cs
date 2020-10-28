using System.Collections;
using System.Collections.Generic;
using System.IO;
using Events;
using NUnit.Framework;
using Singletons;
using Structs;
using UnityEngine;
using UnityEngine.TestTools;
using Utility;
using VDFramework.EventSystem;

namespace Tests
{
    public class TestNewGame
    {
		private const int StartMoney = 10000;
		
		private readonly string destination = Application.persistentDataPath + "/save.dat";
		
		private GameData gameData = new GameData("", "", StartMoney, true, 1f, 1f,
			new Dictionary<Vector2IntSerializable, TileData>());
		
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
				Assert.AreEqual(StartMoney, UserSettings.GameData.Money);
			}
			else
			{
				Assert.Pass();
			}
		}
	}
}
