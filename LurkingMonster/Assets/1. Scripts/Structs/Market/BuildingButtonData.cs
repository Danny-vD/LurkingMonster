using System;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Interfaces;

namespace Structs.Market
{
	[Serializable]
	public struct BuildingButtonData : IKeyValuePair<BuildingType, Button>, IBuyButtonData
	{
		[SerializeField]
		private BuildingType buildingType;

		[SerializeField]
		private Button button;

		[SerializeField]
		private BuyButtonText text;

		public BuildingType Key
		{
			get => buildingType;
			set => buildingType = value;
		}

		public Button Value
		{
			get => button;
			set => button = value;
		}

		public BuyButtonText Text => text;

		public bool Equals(IKeyValuePair<BuildingType, Button> other) => Key.Equals(other.Key);
	}
}