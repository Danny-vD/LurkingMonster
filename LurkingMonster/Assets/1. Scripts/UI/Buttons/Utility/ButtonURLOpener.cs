using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace UI.Buttons.Utility
{
	[RequireComponent(typeof(URLOpener), typeof(Button))]
	public class ButtonURLOpener : BetterMonoBehaviour
	{
		private URLOpener urlOpener;

		private void Awake()
		{
			urlOpener = GetComponent<URLOpener>();
			
			GetComponent<Button>().onClick.AddListener(urlOpener.OpenURL);
		}
	}
}