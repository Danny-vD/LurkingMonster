using System.Collections.Generic;
using System.Linq;
using Enums;
using ScriptableObjects;
using Structs.Buildings;
using Structs.Buildings.TierData;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace Gameplay.Buildings
{
	public class BuildingSpawner : BetterMonoBehaviour
	{
		//TODO: move all the data to a seperate class, so that the buildingSpawner only spawns it
		[SerializeField]
		private List<SoilDataPerSoilType> soilData = new List<SoilDataPerSoilType>();

		[SerializeField]
		private List<FoundationDataPerFoundationType> foundationData = new List<FoundationDataPerFoundationType>();

		[SerializeField]
		private List<BuildingTierDataPerBuildingType> buildingTierData = new List<BuildingTierDataPerBuildingType>();

		[SerializeField, Header("All spawnpoints are relative")]
		private Vector3 soilSpawnpoint;

		[SerializeField]
		private Vector3 foundationSpawnpoint;

		[SerializeField]
		private Vector3 buildingSpawnpoint;

		private void Awake()
		{
			Vector3 position = CachedTransform.TransformPoint(CachedTransform.position);
		}

		public GameObject SpawnSoil(SoilType soilType)
		{
			GameObject prefab = soilData.First(pair => pair.Key.Equals(soilType)).Value.Prefabs.GetRandomItem();
			GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation);

			instance.transform.Translate(soilSpawnpoint, Space.World);

			instance.name = soilType.ToString().ReplaceUnderscoreWithSpace();

			return instance;
		}

		public GameObject SpawnFoundation(FoundationType foundationType)
		{
			GameObject prefab = foundationData.First(pair => pair.Key.Equals(foundationType)).Value.Prefabs.GetRandomItem();
			GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation);

			instance.transform.Translate(foundationSpawnpoint, Space.World);

			instance.name = foundationType.ToString().ReplaceUnderscoreWithSpace();

			return instance;
		}

		public Building SpawnBuilding(BuildingType buildingType, FoundationType foundationType, SoilType soilType)
		{
			GameObject prefab = buildingTierData.First(pair => pair.Key.Equals(buildingType)).Value[0].GetPrefab();
			GameObject instance = Instantiate(prefab, CachedTransform.position, CachedTransform.rotation, CachedTransform);

			instance.transform.Translate(buildingSpawnpoint, Space.World);

			instance.name = buildingType.ToString().InsertSpaceBeforeCapitals();

			Building building = instance.GetComponent<Building>();
			building.Initialize(buildingType,
				GetBuildingData(buildingType, foundationType, soilType), GetFoundationData(foundationType), GetSoilData(soilType));

			return building;
		}

		public SoilTypeData GetSoilData(SoilType soilType)
		{
			return soilData.First(pair => pair.Key.Equals(soilType)).Value;
		}

		public FoundationTypeData GetFoundationData(FoundationType foundationType)
		{
			return foundationData.First(pair => pair.Key.Equals(foundationType)).Value;
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