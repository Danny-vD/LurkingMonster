using Structs;
using VDFramework;

namespace Gameplay
{
	public class Building : BetterMonoBehaviour
	{
		/// <summary>
		///	Returns the BuildingData of the current building
		/// <para> Be sure to only get this in OnEnable or later </para>
		/// </summary>
		public BuildingData Data { get; private set; }
		
		public void Instantiate(BuildingData buildingData)
		{
			Data = buildingData;
		}
	}
}