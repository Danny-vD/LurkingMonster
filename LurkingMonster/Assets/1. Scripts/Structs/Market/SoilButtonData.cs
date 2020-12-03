using System;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Interfaces;

namespace Structs.Market
{
	[Serializable]
	public struct SoilButtonData : IKeyValuePair<SoilType, Button>, IBuyButtonData
	{
		[SerializeField]
		private SoilType soilType;

		[SerializeField]
		private Button button;

		[SerializeField]
		private BuyButtonText text;

		public SoilType Key
		{
			get => soilType;
			set => soilType = value;
		}

		public Button Value
		{
			get => button;
			set => button = value;
		}

		public BuyButtonText Text => text;

		public bool Equals(IKeyValuePair<SoilType, Button> other) => Key.Equals(other.Key);
	}
}