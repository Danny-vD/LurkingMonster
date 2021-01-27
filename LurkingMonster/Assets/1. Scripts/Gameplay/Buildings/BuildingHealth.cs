using System;
using Enums;
using Events.BuildingEvents.RepairEvents;
using ScriptableObjects;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	/// <summary>
	/// A class that is responsible for keeping track of the health of the seperate building components
	/// </summary>
	public class BuildingHealth : BetterMonoBehaviour
	{
		public float MaxSoilHealth { get; private set; }
		public float MaxFoundationHealth { get; private set; }
		public float MaxBuildingHealth { get; private set; }

		public float CurrentSoilHealth { get; private set; }
		public float CurrentSoilHealthPercentage => CurrentSoilHealth / MaxSoilHealth;

		public float CurrentFoundationHealth { get; private set; }
		public float CurrentFoundationHealthPercentage => CurrentFoundationHealth / MaxFoundationHealth;

		public float CurrentBuildingHealth { get; private set; }
		public float CurrentBuildingHealthPercentage => CurrentBuildingHealth / MaxBuildingHealth;

		public float TotalHealth => CurrentSoilHealth + CurrentFoundationHealth + CurrentBuildingHealth;
		public float MaxTotalHealth => MaxSoilHealth + MaxFoundationHealth + MaxBuildingHealth;

		public event Action OnBuildingRepair;
		public event Action OnFoundationRepair;
		public event Action OnSoilRepair;

		private void Awake()
		{
			GetComponent<Building>().OnInitialize += Initialize;
		}

		private void Initialize(BuildingData buildingData, FoundationTypeData foundationData, SoilTypeData soilData)
		{
			SetMaxSoilHealth(soilData.MaxHealth);
			SetMaxFoundationHealth(foundationData.MaxHealth);
			SetMaxBuildingHealth(buildingData.MaxHealth);

			ResetHealth(false);
		}

		public void SetCurrentHealth(float soilHealth, float foundationHealth, float buildingHealth)
		{
			CurrentSoilHealth       = soilHealth;
			CurrentFoundationHealth = foundationHealth;
			CurrentBuildingHealth   = buildingHealth;
		}

		public void ResetHealth(bool raiseEvent)
		{
			ResetSoilHealth(raiseEvent);
			ResetFoundationHealth(raiseEvent);
			ResetBuildingHealth(raiseEvent);
		}

		public bool DamageSoil(float damage)
		{
			CurrentSoilHealth -= damage;

			return CurrentSoilHealth <= 0;
		}

		public bool DamageFoundation(float damage)
		{
			CurrentFoundationHealth -= damage;

			return CurrentFoundationHealth <= 0;
		}

		public bool DamageBuilding(float damage)
		{
			CurrentBuildingHealth -= damage;

			return CurrentBuildingHealth <= 0;
		}

		public void SetMaxSoilHealth(float maxHealth)
		{
			MaxSoilHealth = maxHealth;
		}

		public void SetMaxFoundationHealth(float maxHealth)
		{
			MaxFoundationHealth = maxHealth;
		}

		public void SetMaxBuildingHealth(float maxHealth)
		{
			MaxBuildingHealth = maxHealth;
		}

		public void ResetSoilHealth(bool raiseEvent = true)
		{
			CurrentSoilHealth = MaxSoilHealth;
			OnSoilRepair?.Invoke();

			if (raiseEvent)
			{
				EventManager.Instance.RaiseEvent(new SoilRepairEvent());
			}
		}

		public void ResetFoundationHealth(bool raiseEvent = true)
		{
			CurrentFoundationHealth = MaxFoundationHealth;
			OnFoundationRepair?.Invoke();

			if (raiseEvent)
			{
				EventManager.Instance.RaiseEvent(new FoundationRepairEvent());
			}
		}

		public void ResetBuildingHealth(bool raiseEvent = true)
		{
			CurrentBuildingHealth = MaxBuildingHealth;
			OnBuildingRepair?.Invoke();

			if (raiseEvent)
			{
				EventManager.Instance.RaiseEvent(new BuildingRepairEvent());
			}
		}

		public bool IsHealthBelowLimit(float percentage)
		{
			if (CurrentBuildingHealth <= MaxBuildingHealth / 100 * percentage)
			{
				return true;
			}

			if (CurrentFoundationHealth <= MaxFoundationHealth / 100 * percentage)
			{
				return true;
			}

			if (CurrentSoilHealth <= MaxSoilHealth / 100 * percentage)
			{
				return true;
			}

			return false;
		}

		public void SetBuildingHealthBar(Bar bar)
		{
			bar.SetMax((int) MaxBuildingHealth);
			bar.SetValue((int) CurrentBuildingHealth);
		}

		public void SetFoundationHealthBar(Bar bar)
		{
			bar.SetMax((int) MaxFoundationHealth);
			bar.SetValue((int) CurrentFoundationHealth);
		}

		public void SetSoilHealthBar(Bar bar)
		{
			bar.SetMax((int) MaxSoilHealth);
			bar.SetValue((int) CurrentSoilHealth);
		}

		public BreakType SetLowestHealthBar(Bar bar)
		{
			float buildingPercentage = CurrentBuildingHealth / MaxBuildingHealth;
			float foundationPercentage = CurrentFoundationHealth / MaxFoundationHealth;
			float soilPercentage = CurrentSoilHealth / MaxSoilHealth;

			if (buildingPercentage < foundationPercentage) // Building < Foundation
			{
				if (buildingPercentage < soilPercentage) // Building < Soil
				{
					SetBuildingHealthBar(bar);
					return BreakType.Building;
				}
			}
			else if (foundationPercentage < soilPercentage) // Foundation < Soil
			{
				SetFoundationHealthBar(bar);
				return BreakType.Foundation;
			}

			SetSoilHealthBar(bar); // Soil < Foundation && Soil < Building
			return BreakType.Soil;
		}
	}
}