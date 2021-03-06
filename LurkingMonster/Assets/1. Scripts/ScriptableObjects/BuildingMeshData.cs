﻿using System.Collections.Generic;
using System.Linq;
using Enums;
using Structs.Buildings.MeshData;
using UnityEngine;
using VDFramework.Utility;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/Building Mesh Data")]
	public class BuildingMeshData : ScriptableObject
	{
		[SerializeField]
		private List<BuildingTierMeshPerBuildingType> buildingTierMeshPerBuildingTypes =
			new List<BuildingTierMeshPerBuildingType>();

		public TierMeshData GetMeshData(BuildingType buildingType, int tier)
		{
			return buildingTierMeshPerBuildingTypes.First(pair => pair.Key.Equals(buildingType)).Value[tier - 1];
		}

		public int GetMaxTier(BuildingType buildingType)
		{
			return buildingTierMeshPerBuildingTypes.First(pair => pair.Key.Equals(buildingType)).Value.Count;
		}

#if UNITY_EDITOR
		public void PopulateDictionaries()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<BuildingTierMeshPerBuildingType, BuildingType, List<TierMeshData>>(
				buildingTierMeshPerBuildingTypes);
		}
#endif
	}
}