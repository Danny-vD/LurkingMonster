using Enums;

namespace Utility
{
	public static class Switches
	{
		public static float SoilTypeSwitch(SoilType soilType)
		{
			switch (soilType)
			{
				case SoilType.Sand:
					return 300.0f;
				case SoilType.Loam:
					return 240.0f;
				case SoilType.Clay:
					return 180.0f;
				case SoilType.Peat:
					return 120.0f;
				default:
					return 0.0f;
			}
		}

		public static float FoundationTypeSwitch(FoundationType foundationType)
		{
			switch (foundationType)
			{
				case FoundationType.Wooden_Poles:
					return 300.0f;
				case FoundationType.Shallow_Foundation:
					return 240.0f;
				case FoundationType.Reinforced_Concrete:
					return 180.0f;
				case FoundationType.Floating_Floor_Plate:
					return 120.0f;
				default:
					return 0.0f;
			}
		}
	}
}