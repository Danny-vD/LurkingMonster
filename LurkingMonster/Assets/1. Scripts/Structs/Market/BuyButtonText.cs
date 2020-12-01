using System;
using TMPro;
using UnityEngine;

namespace Structs.Market
{
	[Serializable]
	public struct BuyButtonText
	{
		[Header("Only assign what is necessary")]
		public TextMeshProUGUI Type;

		public TextMeshProUGUI Price;

		[Space(5)]
		public TextMeshProUGUI Rent;

		public TextMeshProUGUI Health;
		public TextMeshProUGUI Upgrades;
	}
}