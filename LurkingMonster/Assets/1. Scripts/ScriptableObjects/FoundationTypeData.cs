using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/FoundationType Data")]
	public class FoundationTypeData : ScriptableObject
	{
		public GameObject[] Prefabs = new GameObject[0];
		
		[Space]
		public int BuildCost = 200;

		public int DestroyCost = 500;
	}
}