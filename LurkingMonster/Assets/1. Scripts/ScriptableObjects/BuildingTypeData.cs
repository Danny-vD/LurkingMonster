using Structs;
using Structs.Buildings;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "BuildingType Data")]
	public class BuildingTypeData : ScriptableObject
	{
		[SerializeField, Tooltip("The percentage of the price that will be collected as rent")]
		private int rentPercentage = 30;

		[SerializeField]
		private int weight = 100;

		[SerializeField]
		private int price = 10;

		public BuildingData GetStruct()
		{
			return new BuildingData(GetPricePercentage(rentPercentage), weight, price, default, default);
		}

		private int GetPricePercentage(int percentage)
		{
			return (int) (percentage / 100.0f * price);
		}
	}
}