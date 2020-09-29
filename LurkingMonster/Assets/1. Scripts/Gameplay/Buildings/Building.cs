using System;
using Enums;
using Structs.Buildings;
using VDFramework;

namespace Gameplay.Buildings
{
	public class Building : BetterMonoBehaviour
	{
		private BuildingData[] data;

		/// <summary>
		///	Returns the BuildingData of the current tier of the building
		/// <para> Be sure to only get this in OnEnable or later </para>
		/// <para> Keep in mind to not cache it, as it updates per tier</para>
		/// </summary>
		public BuildingData Data => data[CurrentTier - 1]; // tier is one-indexed

		public int UpgradeCost => CalculateUpgradeCost();

		/// <summary>
		/// The current tier of the building (one-indexed)
		/// </summary>
		public int CurrentTier { get; set; } = 1;
		
		public BuildingType BuildingType { get; private set; }
		
		public void Instantiate(BuildingType type, BuildingData[] buildingData)
		{
			BuildingType = type;
			data = buildingData;
		}

		private int CalculateUpgradeCost()
		{
			if (CurrentTier >= data.Length)
			{
				throw new Exception("An attempt was made to upgrade while the building can not upgrade any further");
			}

			return data[CurrentTier].Price; // CurrentTier is one-indexed so using it as an index will point to the next tier
		}
	}
}