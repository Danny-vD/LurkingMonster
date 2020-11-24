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

		public TextMeshProUGUI TypeText;
		
		public TextMeshProUGUI StatsText;

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
		
		//TODO: split up in smaller structs for typeText and StatsText......
		// Have Methods to enable or disable?
	}
}