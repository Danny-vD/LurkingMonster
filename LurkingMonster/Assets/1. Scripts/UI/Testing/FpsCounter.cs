using System;
using System.Globalization;
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
			fps.text = ((int) (1.0f / Time.unscaledTime)).ToString(CultureInfo.CurrentCulture);
		}
	}
}