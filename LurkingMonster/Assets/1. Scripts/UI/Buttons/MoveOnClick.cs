using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class MoveOnClick : BetterMonoBehaviour
	{
		[SerializeField]
		private Vector3 TranslateVector = new Vector3(0, -200, 0);

		[SerializeField]
		private List<RectTransform> transforms = new List<RectTransform>();

		private bool hasMoved;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(MoveTransforms);
		}

		private void MoveTransforms()
		{
			hasMoved ^= true;
			
			if (hasMoved)
			{
				transforms.ForEach(MoveBack);
				return;
			}

			transforms.ForEach(Move);
		}

		private void Move(RectTransform rectTransform)
		{
			rectTransform.Translate(TranslateVector);
		}

		private void MoveBack(RectTransform rectTransform)
		{
			rectTransform.Translate(-TranslateVector);
		}
	}
}