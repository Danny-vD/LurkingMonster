using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utility;
using VDFramework;

namespace Gameplay.Buildings
{
	/// <summary>
	/// A class to set the canvas and smoke and whatever else correctly per building type and tier
	/// </summary>
	[RequireComponent(typeof(Building))]
	public class PlaceObjectsCorrectly : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableEnumDictionary<BuildingType, SerializableDictionary<int, SerializableDictionary<Transform, Vector3>>>
			positionsPerTier;

		private Building building;
		private BuildingUpgrade buildingUpgrade;

		private void Awake()
		{
			building        = GetComponent<Building>();
			buildingUpgrade = GetComponent<BuildingUpgrade>();

			building.OnInitialize     += Initialize;
			buildingUpgrade.OnUpgrade += SetPositions;
		}

		private void OnDestroy()
		{
			building.OnInitialize -= Initialize;
			buildingUpgrade.OnUpgrade -= SetPositions;
		}

		private void Initialize(BuildingData buildingData, FoundationTypeData foundationTypeData, SoilTypeData soilTypeData)
		{
			SetPositions();
		}

		private void SetPositions()
		{
			Dictionary<Transform, Vector3> positions = positionsPerTier[building.BuildingType][building.CurrentTier];

			foreach (KeyValuePair<Transform, Vector3> pair in positions)
			{
				pair.Key.localPosition = pair.Value;
			}
		}
	}
}