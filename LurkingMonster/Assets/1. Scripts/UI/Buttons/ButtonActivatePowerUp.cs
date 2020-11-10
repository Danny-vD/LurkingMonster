using Enums;
using Singletons;
using VDFramework;

namespace UI.Buttons
{
	public class ButtonActivatePowerUp : BetterMonoBehaviour
	{
		public void ActivatePowerUp()
		{
			PowerUpManager.Instance.ActivatePowerUp(PowerUpType.AvoidWeatherEvent);
		}
	}
}