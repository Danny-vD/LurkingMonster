using Structs;
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
			return new BuildingData((int) (rentPercentage / 100.0f * price), weight, price, default, default);
		}
	}
}