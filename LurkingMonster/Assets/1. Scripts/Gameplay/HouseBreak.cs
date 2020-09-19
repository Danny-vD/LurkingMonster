using Structs;
using UnityEngine;

namespace Gameplay
{
	public class HouseBreak : MonoBehaviour
	{
		public float TimeForHouseToBreak = 5.0f;
		public float Timer = 0.0f;

		private HouseData houseData;

		private int soilSeconds;
		private int foundationSeconds;
		// Start is called before the first frame update
		void Start()
		{
			House house = GetComponent<House>();
			houseData         = house.Data;
			soilSeconds       = (int) houseData.SoilType;
			foundationSeconds = (int) houseData.Foundation;
			print(soilSeconds);
		}

		// Update is called once per frame
		void Update()
		{
			Timer += Time.deltaTime;
		
			if (Timer > (TimeForHouseToBreak + foundationSeconds))
			{
				//print(Timer);
				Timer = 0.0f;
			}
		}
	}
}
