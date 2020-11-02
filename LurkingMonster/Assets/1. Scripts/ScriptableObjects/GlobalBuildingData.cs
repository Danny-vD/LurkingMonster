using UnityEngine;

namespace ScriptableObjects
{
	/// <summary>
	/// Building Data that is the same for every building
	/// </summary>
	[CreateAssetMenu(menuName = "Building Data/Global Data")]
	public class GlobalBuildingData : ScriptableObject
	{
		public int DestructionCost; //TODO: Move to buildingData since it's suddenly different per building
	}
}