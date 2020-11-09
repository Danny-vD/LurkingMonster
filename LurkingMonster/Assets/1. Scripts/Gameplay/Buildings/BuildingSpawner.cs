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
		private List<SoilDataPerSoilType> soilData = new List<SoilDataPerSoilType>();
		
		[SerializeField]
		private List<FoundationDataPerFoundationType> foundationData = new List<FoundationDataPerFoundationType>();

		[SerializeField]
		private List<BuildingTierDataPerBuildingType> buildingTierData = new List<BuildingTierDataPerBuildingType>();

		public Building Spawn(BuildingType buildingType, FoundationType foundationType, SoilType soilType)
		{
			GameObject prefab = buildingTierData.First(pair => pair.Key.Equals(buildingType)).Value[0].GetPrefab();
			GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation, CachedTransform);

			instance.name = buildingType.ToString().InsertSpaceBeforeCapitals();

			Building building = instance.GetComponent<Building>();
			building.Initialize(buildingType, GetBuildingData(buildingType, foundationType, soilType));

			//TODO: make an option to not raise event
			EventManager.Instance.RaiseEvent(new BuildingBuildEvent());

			return building;
		}

		public BuildingData[] GetBuildingData(BuildingType buildingType, FoundationType foundationType, SoilType soilType)
		{
			List<BuildingTierData> buildingTypeData = buildingTierData.First(pair => pair.Key.Equals(buildingType)).Value;
			BuildingData[] data = new BuildingData[buildingTypeData.Count];

			for (int i = 0; i < buildingTypeData.Count; i++)
			{
				BuildingData datum = buildingTypeData[i].GetStruct();

				datum.SoilType   = soilType;
				datum.Foundation = foundationType;

				data[i] = datum;
			}

			return data;
		}

		public FoundationTypeData GetFoundationData(FoundationType foundationType)
		{
			return foundationData.First(pair => pair.Key.Equals(foundationType)).Value;
		}

		public GameObject SpawnFoundation(FoundationType foundationType)
		{
			GameObject prefab = foundationData.First(pair => pair.Key.Equals(foundationType)).Value.Prefabs.GetRandomItem();
			GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation);

			instance.name = foundationType.ToString().InsertSpaceBeforeCapitals();

			return instance;
		}

#if UNITY_EDITOR
		public void PopulateDictionaries()
		{
			EnumDictionaryUtil
				.PopulateEnumDictionary<FoundationDataPerFoundationType, FoundationType, FoundationTypeData>(foundationData);

			EnumDictionaryUtil
				.PopulateEnumDictionary<BuildingTierDataPerBuildingType, BuildingType, List<BuildingTierData>>(buildingTierData);
			
			EnumDictionaryUtil
				.PopulateEnumDictionary<SoilDataPerSoilType, SoilType, SoilTypeData>(soilData);
		}
#endif
	}
}