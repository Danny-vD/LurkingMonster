using Enums;
using Structs;
using UnityEngine;
using Utility;
using VDFramework;

namespace Gameplay
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		public float Health;
		public HealthBar healthBar;
		private BuildingData buildingData;

		private float timer = 0.0f;
		// Start is called before the first frame update
		private void Start()
		{
			Building building = GetComponent<Building>();
			buildingData           =  building.Data;
			CalculateBuildingBreakTime();
			healthBar.SetMaxHealth((int) Health);
		}

		// Update is called once per frame
		private void Update()
		{
			timer  += Time.deltaTime;
			Health -= Time.deltaTime;
			
			healthBar.SetHealth((int) Health);

			if (Health <= 0)
			{
				timer = 0.0f;
				print("Het werkt!!!!!!!");
			}
		}

		public void CalculateBuildingBreakTime()
		{
			Health =  0.0f;
			Health += Switches.SoilTypeSwitch(buildingData.SoilType);
			Health += Switches.FoundationTypeSwitch(buildingData.Foundation);
		}
	}
}