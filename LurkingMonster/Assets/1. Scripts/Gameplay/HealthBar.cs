using UnityEngine.UI;
using VDFramework;

namespace Gameplay
{
	public class HealthBar : BetterMonoBehaviour
	{
		public Slider slider;

		public void SetMaxHealth(int health)
		{
			slider.maxValue = health;
			slider.value    = health;
		}
		
		public void SetHealth(int health)
		{
			slider.value = health;
		}
	}
}