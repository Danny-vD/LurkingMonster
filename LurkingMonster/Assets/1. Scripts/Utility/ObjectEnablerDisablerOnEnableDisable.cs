using System;
using System.Collections.Generic;
using UnityEngine;
using VDFramework;

namespace Utility
{
	public class ObjectEnablerDisablerOnEnableDisable : BetterMonoBehaviour
	{
		[SerializeField]
		private List<GameObject> objects = new List<GameObject>();

		private void OnEnable()
		{
			objects.ForEach(Disable);
		}

		private void OnDisable()
		{
			objects.ForEach(Enable);
		}

		private static void Enable(GameObject @object)
		{
			@object.SetActive(true);
		}
		
		private static void Disable(GameObject @object)
		{
			@object.SetActive(false);
		}
	}
}
