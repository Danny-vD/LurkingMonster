using System;
using UnityEngine;
using UnityEngine.UI;

namespace Structs.Market
{
	[Serializable]
	public struct BuyButtonData : IEquatable<BuyButtonData>
	{
		[SerializeField]
		private Button button;

		[SerializeField]
		private BuyButtonText text;

		public Button Button
		{
			get => button;
			set => button = value;
		}

		public BuyButtonText Text => text;

		public bool Equals(BuyButtonData other) => button.Equals(other.button);
	}
}