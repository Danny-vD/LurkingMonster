using System.Collections.Generic;
using System.Linq;
using Enums;
using Events;
using ScriptableObjects;
using Structs.Buildings;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace Gameplay.Buildings
{
    public class BuildingSpawner : BetterMonoBehaviour
    {
		[SerializeField]
		private List<PrefabPerFoundationType> foundations = new List<PrefabPerFoundationType>();
		
		[SerializeField]
		private List<FoundationDataPerFoundationType> foundationData = new List<FoundationDataPerFoundationType>();
        
		[SerializeField]
        private List<PrefabPerBuildingType> buildings = new List<PrefabPerBuildingType>();

        [SerializeField]
        private List<BuildingDataPerBuildingType> buildingData = new List<BuildingDataPerBuildingType>();

		public Building Spawn(BuildingType buildingType, FoundationType foundationType, SoilType soilType)
        {
            GameObject prefab = buildings.First(pair => pair.Key.Equals(buildingType)).Value;
            GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation);

			instance.name = buildingType.ToString().InsertSpaceBeforeCapitals();
			
            Building building = instance.GetComponent<Building>();
            building.Instantiate(buildingType, GetBuildingData(buildingType, foundationType, soilType));
			
			EventManager.Instance.RaiseEvent(new BuildingBuildEvent());

			return building;
		}

		public BuildingData[] GetBuildingData(BuildingType houseType, FoundationType foundationType, SoilType soilType)
		{
			List<BuildingTypeData> buildingTypeData = buildingData.First(pair => pair.Key.Equals(houseType)).Value;
			BuildingData[] data = new BuildingData[buildingTypeData.Count];

			for (int i = 0; i < buildingTypeData.Count; i++)
			{
				data[i] = buildingTypeData[i].GetStruct();
				data[i].Foundation = foundationType;
				data[i].SoilType = soilType;
			}

			return data;
		}

		public FoundationTypeData GetFoundationData(FoundationType foundationType)
		{
			return foundationData.First(pair => pair.Key.Equals(foundationType)).Value;
		}

		public GameObject SpawnFoundation(FoundationType foundationType)
		{
			GameObject prefab = foundations.First(pair => pair.Key.Equals(foundationType)).Value;
			GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation);

			instance.name = foundationType.ToString().InsertSpaceBeforeCapitals();

			return instance;
		}

#if UNITY_EDITOR
        public void PopulateDictionaries()
        {
            EnumDictionaryUtil.PopulateEnumDictionary<PrefabPerFoundationType, FoundationType, GameObject>(foundations);
            EnumDictionaryUtil.PopulateEnumDictionary<FoundationDataPerFoundationType, FoundationType, FoundationTypeData>(foundationData);
            EnumDictionaryUtil.PopulateEnumDictionary<PrefabPerBuildingType, BuildingType, GameObject>(buildings);
            EnumDictionaryUtil.PopulateEnumDictionary<BuildingDataPerBuildingType, BuildingType, List<BuildingTypeData>>(buildingData);
        }
#endif
    }
}