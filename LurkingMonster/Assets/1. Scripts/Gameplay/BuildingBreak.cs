using Enums;
using Structs;
using UnityEngine;
using Utility;
using VDFramework;

namespace Gameplay
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		public float TimeForBuildingToBreak = 0.0f;
		public HealthBar healthBar;
		private BuildingData buildingData;

		private float timer = 0.0f;
		// Start is called before the first frame update
		private void Start()
		{
			Building building = GetComponent<Building>();
			buildingData           =  building.Data;
			CalculateBuildingBreakTime();
			healthBar.SetMaxHealth((int) TimeForBuildingToBreak);
		}

		// Update is called once per frame
		private void Update()
		{
			timer                  += Time.deltaTime;
			TimeForBuildingToBreak -= Time.deltaTime;
			healthBar.SetHealth((int) TimeForBuildingToBreak);
			print(TimeForBuildingToBreak);
			if (timer > (TimeForBuildingToBreak / 50))
			{
				//print(Timer);
				print(TimeForBuildingToBreak);
				
				timer = 0.0f;
			}
		}

		public void CalculateBuildingBreakTime()
		{
			TimeForBuildingToBreak =  0.0f;
			TimeForBuildingToBreak += Switches.SoilTypeSwitch(buildingData.SoilType);
			TimeForBuildingToBreak += Switches.FoundationTypeSwitch(buildingData.Foundation);
		}
	}
}