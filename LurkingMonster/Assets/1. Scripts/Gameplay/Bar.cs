using UnityEngine.UI;
using VDFramework;

namespace Gameplay
{
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