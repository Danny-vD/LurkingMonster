using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Interfaces;

namespace Structs.Market
{
	[Serializable]
	public struct BuildingButtonData : IKeyValuePair<BuildingType, Button>
	{
		[SerializeField]
		private BuildingType buildingType;

		[SerializeField]
		private Button button;

		public Text Texts;

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

		public bool Equals(IKeyValuePair<BuildingType, Button> other) => Key.Equals(other.Key);

		[Serializable]
		public struct Text
		{
			public TextMeshProUGUI Type;
			public TextMeshProUGUI Price;

			[Space(5)]
			public TextMeshProUGUI Rent;

			public TextMeshProUGUI Health;
			public TextMeshProUGUI Upgrades;
		}
	}
}