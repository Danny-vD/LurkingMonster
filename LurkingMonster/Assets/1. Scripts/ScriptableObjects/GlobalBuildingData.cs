using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/Global Data")]
	public class GlobalBuildingData : ScriptableObject
	{
		public int DestructionCost;
	}
}