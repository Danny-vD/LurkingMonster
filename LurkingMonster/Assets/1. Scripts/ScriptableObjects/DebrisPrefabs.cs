using System.Collections.Generic;
using System.Linq;
using Enums;
using Structs.Buildings;
using Structs.Buildings.TierData;
using UnityEngine;
using VDFramework.Utility;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/Debris Data")]
	public class DebrisPrefabs : ScriptableObject
	{
		[SerializeField]
		private List<DebrisPerBuilding> debrisPerBuilding = new List<DebrisPerBuilding>();

		public GameObject GetPrefab(BuildingType buildingType, int buildingTier)
		{
			return debrisPerBuilding.First(pair => pair.Key.Equals(buildingType)).Value[buildingTier];
		}

		public void PopulateDictionary()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<DebrisPerBuilding, BuildingType, List<GameObject>>(debrisPerBuilding);
		}
	}
}