using VDFramework;

namespace UI.Buttons
{
	public class ButtonFunctionality : BetterMonoBehaviour
	{
		public void QuitApplication()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
			UnityEngine.Application.Quit();
#endif
		}

		public void LoadScene(string scene)
		{
			LoadingScreen.LoadScene(scene);
		}

		public void LoadScene(int buildIndex)
		{
			LoadingScreen.LoadScene(buildIndex);
		}
	}
}