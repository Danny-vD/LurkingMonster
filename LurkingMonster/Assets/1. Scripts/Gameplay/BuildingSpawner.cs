using System.Collections.Generic;
using System.Linq;
using Enums;
using ScriptableObjects;
using Structs;
using UnityEngine;
using UnityEngine.Serialization;
using VDFramework;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace Gameplay
{
    public class BuildingSpawner : BetterMonoBehaviour
    {
		[SerializeField]
        private List<PrefabPerBuildingType> buildings = new List<PrefabPerBuildingType>();

        [SerializeField]
        private List<BuildingDataPerBuildingType> buildingData = new List<BuildingDataPerBuildingType>();

		public void Spawn(BuildingType houseType, FoundationType foundationType, SoilType soilType)
        {
            GameObject prefab = buildings.First(pair => pair.Key.Equals(houseType)).Value;
            GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation);

			instance.name = houseType.ToString().InsertSpaceBeforeCapitals();
			
            Building house = instance.GetComponent<Building>();
            house.Instantiate(GetData(houseType, foundationType, soilType));
        }

		public BuildingData GetBuildingData(BuildingType houseType)
		{
			BuildingTypeData houseTypeData = buildingData.First(pair => pair.Key.Equals(houseType)).Value;

			return houseTypeData.GetStruct();
		}
		
		private BuildingData GetData(BuildingType houseType, FoundationType foundationType, SoilType soilType)
        {
			BuildingData data = GetBuildingData(houseType);
            data.Foundation = foundationType;
            data.SoilType = soilType;

            return data;
        }

#if UNITY_EDITOR
        public void PopulateDictionaries()
        {
            EnumDictionaryUtil.PopulateEnumDictionary<PrefabPerBuildingType, BuildingType, GameObject>(buildings);
            EnumDictionaryUtil.PopulateEnumDictionary<BuildingDataPerBuildingType, BuildingType, BuildingTypeData>(buildingData);
        }
#endif
    }
}