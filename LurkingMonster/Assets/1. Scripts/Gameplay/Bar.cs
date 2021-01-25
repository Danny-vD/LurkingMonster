using UnityEngine.UI;
using VDFramework;

namespace Gameplay
{
	/// <summary>
	/// A class that encapulates some Slider functions to easily set the slider Value and MaxValue
	/// </summary>
	public class Bar : BetterMonoBehaviour
	{
		public Slider slider;

		public void SetMax(int progress)
		{
			slider.maxValue       = progress;
			slider.value          = progress;
		}
		
		public void SetValue(int progress)
		{
			slider.value = progress;
		}

		public float MaxValue => slider.maxValue;
	}
}