using System.Collections.Generic;
using System.Linq;
using Enums;
using Structs.Buildings;
using UnityEngine;
using VDFramework.Utility;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Tier Data")]
	public class BuildingTierData : ScriptableObject
	{
		[SerializeField]
		private List<BuildingTierMeshPerBuildingType> buildingTierMeshPerBuildingTypes =
			new List<BuildingTierMeshPerBuildingType>();

		public Mesh GetMesh(BuildingType buildingType, int tier)
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
			EnumDictionaryUtil.PopulateEnumDictionary<BuildingTierMeshPerBuildingType, BuildingType, List<Mesh>>(
				buildingTierMeshPerBuildingTypes);
		}
#endif
	}
}