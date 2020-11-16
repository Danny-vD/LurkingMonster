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

		protected BuildingData FirstTierData; // The building data of the first tier building
		protected BuildingData DestroyedBuildingData;

		private BuildingSpawner spawner;

		private GameObject soilObject;
		private GameObject foundationObject;
		private GameObject debrisObject;

		private MeshRenderer meshRenderer;

		public Building Building { get; private set; }

		public bool HasSoil => soilObject || HasFoundation;
		public bool HasFoundation => foundationObject || Building;
		public bool HasDebris => debrisObject;
		public int DebrisRemovalCost => DestroyedBuildingData.CleanupCosts;

		protected virtual void Awake()
		{
			meshRenderer  = GetComponent<MeshRenderer>();
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

		public virtual void SpawnSoil()
		{
			meshRenderer.enabled = false;

			soilObject = spawner.SpawnSoil(FirstTierData.SoilType);
		}

		public virtual void SpawnFoundation()
		{
			RemoveFoundation(false);
			foundationObject = spawner.SpawnFoundation(FirstTierData.Foundation);
		}

		public virtual void SpawnBuilding()
		{
			RemoveSoil(false);
			RemoveFoundation(false);
			Building = spawner.SpawnBuilding(buildingType, FirstTierData.Foundation, FirstTierData.SoilType);
		}

		public virtual void RemoveSoil(bool payForRemoval)
		{
			Destroy(soilObject);

			meshRenderer.enabled = true;
		}

		public virtual void RemoveFoundation(bool payForRemoval)
		{
			Destroy(foundationObject);
		}

		public virtual void RemoveDebris(bool payForRemoval)
		{
			Destroy(debrisObject);
		}

		public int GetSoilPrice()
		{
			return spawner.GetSoilData(GetSoilType()).BuildCost;
		}
		
		public int GetFoundationPrice()
		{
			return spawner.GetFoundationData(GetFoundationType()).BuildCost;
		}
		
		public int GetBuildingPrice()
		{
			return FirstTierData.Price;
		}

		public SoilTypeData GetSoilData(SoilType soilType)
		{
			return spawner.GetSoilData(soilType);
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

			DestroyedBuildingData = spawner.GetBuildingData(buildingType, GetFoundationType(), GetSoilType())[tier];

			GameObject prefab = debrisMeshes.GetPrefab(buildingType, tier);

			debrisObject = Instantiate(prefab, spawner.CachedTransform.position, spawner.CachedTransform.rotation);
		}
	}
}