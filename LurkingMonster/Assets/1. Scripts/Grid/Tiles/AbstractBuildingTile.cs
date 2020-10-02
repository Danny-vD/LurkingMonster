using System;
using Enums;
using Events;
using Gameplay;
using Gameplay.Buildings;
using Singletons;
using Structs;
using Structs.Buildings;
using UnityEngine;
using VDFramework.EventSystem;

namespace Grid.Tiles
{
	public abstract class AbstractBuildingTile : AbstractTile
	{
		[SerializeField]
		protected BuildingType buildingType = default;

		[SerializeField]
		protected SoilType soilType = default;

		[SerializeField]
		protected FoundationType foundationType = default;

		public Building Building { get; private set; }
		
		protected BuildingData BuildingData; // The building data of the first tier building

		private BuildingSpawner spawner;
		
		private GameObject canvas;

		protected virtual void Awake()
		{
			spawner      = GetComponentInChildren<BuildingSpawner>();
			BuildingData = spawner.GetBuildingData(buildingType, foundationType, soilType)[0];
			
			canvas = CachedTransform.Find("Canvas").gameObject;
			
			EventManager.Instance.AddListener<OpenMarketEvent>(ActivateBuyButton);
		}

		public virtual void SpawnBuilding()
		{
			if (MoneyManager.Instance.PlayerHasEnoughMoney(BuildingData.Price))
			{
				Building = spawner.Spawn(buildingType, foundationType, soilType);
			}
		}

		public void SetBuildingType(BuildingType house)
		{
			buildingType = house;
		}

		public void SetSoilType(SoilType soil)
		{
			soilType = soil;
		}

		public void SetFoundation(FoundationType foundation)
		{
			foundationType = foundation;
		}

		// TODO: IMPROVE.
		private void ActivateBuyButton(OpenMarketEvent openMarketEvent)
		{
			if (openMarketEvent.Building == null)
			{
				if (openMarketEvent.BuildingTile == this)
				{
					canvas.SetActive(true);
					return;
				}
			}
			else
			{
				if (openMarketEvent.Building == Building)
				{
					// Not setting to active so we can't build a second building
					return;
				}
			}
			
			canvas.SetActive(false);
		}
	}
}