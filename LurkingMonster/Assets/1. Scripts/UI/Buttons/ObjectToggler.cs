using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class ObjectToggler : BetterMonoBehaviour
	{
		[SerializeField]
		private List<GameObject> objectsToToggle = new List<GameObject>();

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(ToggleObjects);
		}

		private void ToggleObjects()
		{
			objectsToToggle.ForEach(@object => @object.SetActive(!@object.activeSelf));
		}
	}
}