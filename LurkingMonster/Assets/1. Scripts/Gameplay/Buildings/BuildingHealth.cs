using System;
using ScriptableObjects;
using VDFramework;

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
		public float CurrentFoundationHealth { get; private set; }
		public float CurrentBuildingHealth { get; private set; }
		
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
			SetMaxSoilHealth(buildingData.MaxHealth);
			SetMaxFoundationHealth(foundationData.MaxHealth);
			SetMaxBuildingHealth(soilData.MaxHealth);

			ResetHealth();
		}

		public void SetCurrentHealth(float soilHealth, float foundationHealth, float buildingHealth)
		{
			CurrentSoilHealth       = soilHealth;
			CurrentFoundationHealth = foundationHealth;
			CurrentBuildingHealth   = buildingHealth;
		}

		public void ResetHealth()
		{
			ResetSoilHealth();
			ResetFoundationHealth();
			ResetBuildingHealth();
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

		public void ResetSoilHealth()
		{
			CurrentSoilHealth = MaxSoilHealth;
			OnSoilRepair?.Invoke();
		}

		public void ResetFoundationHealth()
		{
			CurrentFoundationHealth = MaxFoundationHealth;
			OnFoundationRepair?.Invoke();
		}

		public void ResetBuildingHealth()
		{
			CurrentBuildingHealth = MaxBuildingHealth;
			OnBuildingRepair?.Invoke();
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

		public void SetLowestHealthBar(Bar bar)
		{
			if (CurrentBuildingHealth < CurrentFoundationHealth) // Building < Foundation
			{
				if (CurrentBuildingHealth < CurrentSoilHealth) // Building < Soil
				{
					SetBuildingHealthBar(bar);
					return;
				}

				SetSoilHealthBar(bar); // Soil < Building && Soil < Foundation
			}
			else // Foundation < Building
			{
				if (CurrentFoundationHealth < CurrentSoilHealth) // Foundation < Soil
				{
					SetFoundationHealthBar(bar);
					return;
				}

				SetSoilHealthBar(bar); // Soil < Foundation && Soil < Building
			}
		}
	}
}