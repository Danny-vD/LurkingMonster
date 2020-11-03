using Enums;
using Events;
using Gameplay.Buildings;
using UnityEngine;
using VDFramework.EventSystem;

namespace Grid.Tiles.Buildings
{
	using ScriptableObjects;

	public abstract class AbstractBuildingTile : AbstractTile
	{
		[SerializeField]
		protected BuildingType buildingType = default;

		[SerializeField]
		private DebrisPrefabs debrisMeshes = null;

		public Building Building { get; private set; }
		
		protected BuildingData FirstTierData; // The building data of the first tier building
		protected BuildingData DestroyedBuildingData;

		private BuildingSpawner spawner;
		private GameObject foundationObject;
		
		private GameObject debrisObject;

		public bool HasFoundation => foundationObject || Building;
		public bool HasDebris => debrisObject;
		public int DebrisRemovalCost => DestroyedBuildingData.CleanupCosts;
		
		protected virtual void Awake()
		{
			spawner       = GetComponentInChildren<BuildingSpawner>();
			FirstTierData = spawner.GetBuildingData(buildingType, default, default)[0];
		}

		private void Start()
		{
			EventManager.Instance.AddListener<BuildingConsumedEvent>(SpawnBrokenHouseAsset);
		}

		private void OnDisable()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<BuildingConsumedEvent>(SpawnBrokenHouseAsset);
		}

		public virtual void SpawnBuilding()
		{
			RemoveFoundation(false);
			Building = spawner.Spawn(buildingType, FirstTierData.Foundation, FirstTierData.SoilType);
		}

		public virtual void SpawnFoundation()
		{
			RemoveFoundation(false);
			foundationObject = spawner.SpawnFoundation(FirstTierData.Foundation);
		}

		public virtual void RemoveFoundation(bool payForRemoval)
		{
			Destroy(foundationObject);
		}

		public virtual void RemoveDebris(bool payForRemoval)
		{
			Destroy(debrisObject);
		}

		public int GetBuildingPrice()
		{
			return FirstTierData.Price;
		}

		public FoundationTypeData GetFoundationData(FoundationType foundation)
		{
			return spawner.GetFoundationData(foundation);
		}

		public void SetBuildingType(BuildingType building)
		{
			buildingType  = building;
			FirstTierData = spawner.GetBuildingData(buildingType, default, FirstTierData.SoilType)[0];
		}

		public BuildingType GetBuildingType()
		{
			return buildingType;
		}

		public void SetSoilType(SoilType soil)
		{
			FirstTierData.SoilType = soil;
		}

		public SoilType GetSoilType()
		{
			return FirstTierData.SoilType;
		}

		public void SetFoundation(FoundationType foundation)
		{
			FirstTierData.Foundation = foundation;
		}

		public FoundationType GetFoundationType()
		{
			return FirstTierData.Foundation;
		}

		private void SpawnBrokenHouseAsset(BuildingConsumedEvent buildingConsumedEvent)
		{
			Building building = buildingConsumedEvent.Building;

			if (building != Building)
			{
				return;
			}

			SpawnDebris(buildingType, building.CurrentTier);
		}

		public void SpawnDebris(BuildingType buildingType, int buildingTier)
		{
			int tier = Mathf.Max(0, buildingTier - 1);
			
			// Still possible to grab data from the class since the object is only removed at the end of the frame
			DestroyedBuildingData = spawner.GetBuildingData(buildingType, GetFoundationType(), GetSoilType())[tier];

			GameObject prefab = debrisMeshes.GetPrefab(buildingType, tier);

			debrisObject = Instantiate(prefab, spawner.CachedTransform.position, spawner.CachedTransform.rotation);
		}
	}
}