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

		public void Initialize(float newMaxSoilHealth, float newMaxFoundationHealth, float newMaxBuildingHealth)
		{
			SetMaxSoilHealth(newMaxSoilHealth);
			SetMaxFoundationHealth(newMaxFoundationHealth);
			SetMaxBuildingHealth(newMaxBuildingHealth);

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
		}

		public void ResetFoundationHealth()
		{
			CurrentFoundationHealth = MaxFoundationHealth;
		}

		public void ResetBuildingHealth()
		{
			CurrentBuildingHealth = MaxBuildingHealth;
		}
	}
}