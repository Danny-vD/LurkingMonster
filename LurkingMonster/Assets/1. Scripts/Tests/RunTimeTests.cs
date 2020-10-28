using System.Diagnostics;
using System.IO;
using Events;
using Singletons;
using UnityEngine;
using UnityEngine.Assertions;
using VDFramework.EventSystem;

namespace _1._Scripts.Tests
{
	public static class RunTimeTests
	{
		public static void TestStartMoney()
		{
			Assert.AreEqual(10000, MoneyManager.Instance.CurrentMoney);
		}
	}
}