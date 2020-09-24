using Structs;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;

namespace Gameplay
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		public float TimeForBuildingToBreak = 5.0f;

		private BuildingData buildingData;

		private float timer = 0.0f;
		private int soilSeconds;
		private int foundationSeconds;

		// Start is called before the first frame update
		private void Start()
		{
			Building building = GetComponent<Building>();
			buildingData         = building.Data;
			soilSeconds       = (int) buildingData.SoilType.GetRandomValue();
			foundationSeconds = (int) buildingData.Foundation;
		}

		// Update is called once per frame
		private void Update()
		{
			timer += Time.deltaTime;

			if (timer > (TimeForBuildingToBreak + foundationSeconds))
			{
				//print(Timer);
				timer = 0.0f;
			}
		}
	}
}