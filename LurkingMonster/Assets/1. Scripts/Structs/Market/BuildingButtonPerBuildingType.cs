using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Interfaces;

namespace Structs.Market
{
	[Serializable]
	public struct BuildingButtonPerBuildingType : IKeyValuePair<BuildingType, Button>
	{
		[SerializeField]
		private BuildingType buildingType;

		[SerializeField]
		private Button button;

		public BuildingType Key
		{
			get => buildingType;
			set => buildingType = value;
		}

		public Button Value
		{
			get => button;
			set => this.button = value;
		}

		public bool Equals(IKeyValuePair<BuildingType, Button> other) => Key.Equals(other.Key);
	}
}