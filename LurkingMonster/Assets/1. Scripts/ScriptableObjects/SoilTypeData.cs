using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/SoilType Data")]
	public class SoilTypeData : ScriptableObject
	{
		public GameObject[] Prefabs = new GameObject[0];
		
		[Space]
		public int BuildCost = 200;
		
		public int RemoveCost = 100;

		public float MaxHealth = 50.0f;
	}
}