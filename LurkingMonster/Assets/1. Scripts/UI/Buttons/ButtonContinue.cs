using Singletons;
using VDFramework;

namespace UI.Buttons
{
	public class ButtonContinue : BetterMonoBehaviour
	{
		private void Start()
		{
			DisableContinueButton();
		}

		private void DisableContinueButton()
		{
			if (!UserSettings.SettingsExist)
			{
				CachedGameObject.SetActive(false);
			}
		}
	}
}