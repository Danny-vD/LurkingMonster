using Singletons;
using UnityEngine.Assertions;

namespace Tests
{
	public static class RunTimeTests
	{
		public static void TestStartMoney()
		{
			Assert.AreEqual(10000, MoneyManager.Instance.CurrentMoney);
		}
	}
}