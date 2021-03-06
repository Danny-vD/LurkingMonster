﻿using Enums;
using Events.BuildingEvents;
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
		private GameObject[] removeOnBuild;

		[SerializeField]
		private DebrisPrefabs debrisMeshes = null;

		protected BuildingData FirstTierData; // The building data of the first tier building
		protected BuildingData DestroyedBuildingData;

		private BuildingSpawner spawner;

		private GameObject debrisObject;

		private MeshRenderer meshRenderer;

		public GameObject Soil { get; private set; }

		public GameObject Foundation { get; private set; }

		public Building Building { get; private set; }

		public bool HasSoil => Soil || HasFoundation;
		public bool HasFoundation => Foundation || HasBuilding;
		public bool HasBuilding => Building;
		public bool HasDebris => debrisObject;
		public int DebrisRemovalCost => DestroyedBuildingData.CleanupCosts;
		
		public int DestroyedBuildingTier { get; private set; }

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
			ToggleProps(false);

			Soil = spawner.SpawnSoil(FirstTierData.SoilType);
		}

		public virtual void SpawnFoundation()
		{
			RemoveFoundation();
			Foundation = spawner.SpawnFoundation(FirstTierData.Foundation);
		}

		public virtual void SpawnBuilding(bool raiseEvent)
		{
			RemoveFoundation();
			RemoveSoil();
			
			ToggleProps(false);
			Building = spawner.SpawnBuilding(buildingType, FirstTierData.Foundation, FirstTierData.SoilType);

			if (raiseEvent)
			{
				EventManager.Instance.RaiseEvent(new BuildingBuiltEvent());
			}
		}

		public virtual void RemoveSoil()
		{
			Destroy(Soil);
			
			ToggleProps(true);

			meshRenderer.enabled = true;
		}

		public virtual void RemoveFoundation()
		{
			Destroy(Foundation);
		}

		public virtual void RemoveDebris()
		{
			ToggleProps(true);
			Destroy(debrisObject);
		}

		public int GetSoilPrice()
		{
			return GetCurrentSoilData().BuildCost;
		}

		public int GetFoundationPrice()
		{
			return GetCurrentFoundationData().BuildCost;
		}

		public int GetBuildingPrice()
		{
			return GetCurrentFirstTierData().Price;
		}

		public SoilTypeData GetCurrentSoilData()
		{
			return GetSoilData(GetSoilType());
		}

		public SoilTypeData GetSoilData(SoilType soilType)
		{
			return spawner.GetSoilData(soilType);
		}

		public FoundationTypeData GetCurrentFoundationData()
		{
			return spawner.GetFoundationData(GetFoundationType());
		}
		
		public FoundationTypeData GetFoundationData(FoundationType foundation)
		{
			return spawner.GetFoundationData(foundation);
		}

		public BuildingData GetCurrentFirstTierData()
		{
			return FirstTierData;
		}
		
		public BuildingData[] GetBuildingData(BuildingType buildingType)
		{
			return spawner.GetBuildingData(buildingType, GetFoundationType(), GetSoilType());
		}

		public void SetSoilType(SoilType soil)
		{
			FirstTierData.SoilType = soil;
		}

		public SoilType GetSoilType()
		{
			return FirstTierData.SoilType;
		}

		public void SetFoundationType(FoundationType foundation)
		{
			FirstTierData.Foundation = foundation;
		}

		public FoundationType GetFoundationType()
		{
			return FirstTierData.Foundation;
		}
		
		public void SetBuildingType(BuildingType building)
		{
			buildingType  = building;
			FirstTierData = spawner.GetBuildingData(buildingType, FirstTierData.Foundation, FirstTierData.SoilType)[0];
		}

		public BuildingType GetBuildingType()
		{
			return buildingType;
		}

		public void SpawnDebris(BuildingType buildingType, int buildingTier)
		{
			RemoveFoundation();
			RemoveSoil();
			
			ToggleProps(false);
			
			int tier = Mathf.Max(0, buildingTier - 1);

			DestroyedBuildingData = spawner.GetBuildingData(buildingType, GetFoundationType(), GetSoilType())[tier];

			GameObject prefab = debrisMeshes.GetPrefab(buildingType, tier);

			debrisObject = Instantiate(prefab, spawner.CachedTransform.position, spawner.CachedTransform.rotation);
		}
		
		private void SpawnBrokenHouseAsset(BuildingConsumedEvent buildingConsumedEvent)
		{
			Building building = buildingConsumedEvent.Building;

			if (building != Building)
			{
				return;
			}

			DestroyedBuildingTier = building.CurrentTier;
			
			SpawnDebris(buildingType, DestroyedBuildingTier);
		}

		private void ToggleProps(bool active)
		{
			foreach (GameObject @object in removeOnBuild)
			{
				@object.SetActive(active);
			}
		}
	}
}