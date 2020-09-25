using UnityEngine.UI;
using VDFramework;

namespace Gameplay
{
	public class HealthBar : BetterMonoBehaviour
	{
		public Slider slider;

		public float StartingHealth;

		public void SetMaxHealth(int health)
		{
			slider.maxValue     = health;
			slider.value        = health;
			this.StartingHealth = health;
		}
		
		public void SetHealth(int health)
		{
			slider.value = health;
		}

		public float StartingHealth1
		{
			get => StartingHealth;
		}
	}
}