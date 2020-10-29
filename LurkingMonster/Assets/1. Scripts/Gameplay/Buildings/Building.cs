using System;
using Enums;
using Events;
using ScriptableObjects;
using Singletons;
using Structs.Buildings;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class Building : BetterMonoBehaviour
	{
		[SerializeField]
		private GlobalBuildingData globalBuildingData = null;

		private BuildingData[] data;

		/// <summary>
		///	Returns the BuildingData of the current tier of the building
		/// <para> Be sure to only get this in OnEnable or later </para>
		/// <para> Keep in mind to not cache it, as it updates per tier</para>
		/// </summary>
		public BuildingData Data => data[CurrentTier - 1]; // tier is one-indexed

		/// <summary>
		/// Returns data that is the same for every building
		/// </summary>
		public GlobalBuildingData GlobalData => globalBuildingData;

		public int UpgradeCost => CalculateUpgradeCost();

		public bool IsMaxTier => CurrentTier >= data.Length;

		/// <summary>
		/// The current tier of the building (one-indexed)
		/// </summary>
		public int CurrentTier { get; set; } = 1;

		public BuildingType BuildingType { get; private set; }

		public void Instantiate(BuildingType type, BuildingData[] buildingData)
		{
			BuildingType = type;
			data         = buildingData;
		}

		public void RemoveBuilding(bool payForRemoval)
		{
			if (payForRemoval)
			{
				if (!MoneyManager.Instance.PlayerHasEnoughMoney(GlobalData.DestructionCost))
				{
					return;
				}
				
				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(GlobalData.DestructionCost));
			}
			
			Destroy(gameObject);
			EventManager.Instance.RaiseEvent(new BuildingDestroyedEvent());
		}

		private int CalculateUpgradeCost()
		{
			if (IsMaxTier)
			{
				throw new Exception("An attempt was made to upgrade while the building can not upgrade any further");
			}

			return data[CurrentTier].Price; // CurrentTier is one-indexed so using it as an index will point to the next tier
		}
	}
}