using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
	[Serializable]
	public struct DebrisPerBuilding : IKeyValuePair<BuildingType, List<GameObject>>
	{
		[SerializeField]
		private BuildingType key;

		[SerializeField]
		private List<GameObject> value;

		public BuildingType Key
		{
			get => key;
			set => key = value;
		}

		public List<GameObject> Value
		{
			get => value;
			set => this.value = value;
		}

		public bool Equals(IKeyValuePair<BuildingType, List<GameObject>> other) => Key.Equals(other.Key);
	}
}