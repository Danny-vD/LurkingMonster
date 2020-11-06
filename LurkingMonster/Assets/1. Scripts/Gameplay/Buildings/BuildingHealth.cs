using UnityEngine;
using VDFramework;

namespace Gameplay.Buildings
{
	/// <summary>
	/// A class that is responsible for keeping track of the health of the seperate building components
	/// </summary>
	public class BuildingHealth : BetterMonoBehaviour
	{
		private float maxSoilHealth;
		private float maxFoundationHealth;
		private float maxBuildingHealth;

		private float soilHealth;
		private float foundationHealth;
		private float buildingHealth;

		public void Initialize(float newMaxSoilHealth, float newMaxFoundationHealth, float newMaxBuildingHealth)
		{
			SetMaxSoilHealth(newMaxSoilHealth);
			SetMaxFoundationHealth(newMaxFoundationHealth);
			SetMaxBuildingHealth(newMaxBuildingHealth);
		}

		public void SetMaxSoilHealth(float maxHealth)
		{
			maxSoilHealth = maxHealth;
		}

		public void SetMaxFoundationHealth(float maxHealth)
		{
			maxFoundationHealth = maxHealth;
		}

		public void SetMaxBuildingHealth(float maxHealth)
		{
			maxBuildingHealth = maxHealth;
		}
		
		
	}
}