namespace ScriptableObjects
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Building Data/FoundationType Data")]
	public class FoundationTypeData : ScriptableObject
	{
		public int BuildCost = 200;

		public int DestroyCost = 500;
	}
}