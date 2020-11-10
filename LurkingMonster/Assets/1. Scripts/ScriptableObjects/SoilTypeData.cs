using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/SoilType Data")]
	public class SoilTypeData : ScriptableObject
	{
		public Material[] Materials = new Material[0];
		
		[Space]
		public int BuildCost = 200;
		
		public int RemoveCost = 100;

		public float MaxHealth = 50;
	}
}