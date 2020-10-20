using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
	[Serializable]
	public struct BuildingTierMeshPerBuildingType : IKeyValuePair<BuildingType, List<Mesh>>
	{
		[SerializeField]
		private BuildingType key;
		
		[SerializeField]
		private List<Mesh> value;

		public BuildingType Key
		{
			get => key;
			set => key = value;
		}

		public List<Mesh> Value
		{
			get => value;
			set => this.value = value;
		}
		
		public bool Equals(IKeyValuePair<BuildingType, List<Mesh>> other)
		{
			return other.Key.Equals(Key);
		}
	}
}