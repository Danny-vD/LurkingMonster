using Enums;
using Gameplay;
using Singletons;
using Structs;
using UnityEngine;

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

		protected BuildingData BuildingData;

		private BuildingSpawner spawner;

		protected virtual void Awake()
		{
			spawner      = GetComponentInChildren<BuildingSpawner>();
			BuildingData = spawner.GetBuildingData(buildingType);
		}

		public virtual void SpawnBuilding()
		{
			if (MoneyManager.Instance.PlayerHasEnoughMoney(BuildingData.Price))
			{
				spawner.Spawn(buildingType, foundationType, soilType);
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
	}
}