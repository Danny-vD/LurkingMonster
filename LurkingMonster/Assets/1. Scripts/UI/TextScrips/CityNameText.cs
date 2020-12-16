using System;
using Singletons;
using UnityEngine;
using VDFramework;

namespace UI.TextScrips
{
	public class CityNameText : BetterMonoBehaviour
	{
		private TextMesh text;

		private Action setName;

		private void Awake()
		{
			text = GetComponent<TextMesh>();
		}

		private void Update()
		{
			if (string.IsNullOrEmpty(UserSettings.GameData.CityName))
			{
				return;
			}

			text.text = UserSettings.GameData.CityName;
			Destroy(this);
		}
	}
}