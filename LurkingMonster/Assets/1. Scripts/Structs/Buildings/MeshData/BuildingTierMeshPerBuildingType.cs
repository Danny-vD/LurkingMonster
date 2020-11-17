using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings.MeshData
{
	[Serializable]
	public struct BuildingTierMeshPerBuildingType : IKeyValuePair<BuildingType, List<TierMeshData>>
	{
		[SerializeField]
		private BuildingType key;
		
		[SerializeField]
		private List<TierMeshData> value;

		public BuildingType Key
		{
			get => key;
			set => key = value;
		}

		public List<TierMeshData> Value
		{
			get => value;
			set => this.value = value;
		}
		
		public bool Equals(IKeyValuePair<BuildingType, List<TierMeshData>> other)
		{
			return other.Key.Equals(Key);
		}
	}
}