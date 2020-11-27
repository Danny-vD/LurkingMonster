using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Testing
{
	public class FpsCounter : BetterMonoBehaviour
	{
		private Text fps;
		
		private void Start()
		{
			fps                         = GetComponent<Text>();
			Application.targetFrameRate = 60;
		}

		private void Update()
		{
			float currentFps = 1.0f / Time.unscaledDeltaTime;
			fps.text = $"FPS: {currentFps:F1}";
		}
	}
}