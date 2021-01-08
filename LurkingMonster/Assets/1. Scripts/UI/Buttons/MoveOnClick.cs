using System;
using System.Collections.Generic;
using UI.Layout;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class MoveOnClick : BetterMonoBehaviour
	{
		[SerializeField]
		private Vector3 TranslateVector = new Vector3(0, -200, 0);

		[SerializeField, Tooltip("Will move the transforms Height distance in the TranslateVector direction if set")]
		private RectTransform dynamicSize;

		[SerializeField]
		private List<RectTransform> transforms = new List<RectTransform>();

		private bool hasMoved;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(MoveTransforms);
		}

		private void MoveTransforms()
		{
			transforms.ForEach(hasMoved ? MoveBack : (Action<RectTransform>) Move);

			hasMoved ^= true;
		}

		private void Move(RectTransform rectTransform)
		{
			if (dynamicSize)
			{
				Vector3 translate = GetDynamicVector();
				rectTransform.Translate(translate);
			}
			else
			{
				rectTransform.Translate(TranslateVector);
			}
		}

		private void MoveBack(RectTransform rectTransform)
		{
			if (dynamicSize)
			{
				Vector3 translate = GetDynamicVector();
				rectTransform.Translate(-translate);
			}
			else
			{
				rectTransform.Translate(-TranslateVector);
			}
		}

		private Vector3 GetDynamicVector()
		{
			SizeFitter sizeFitter = dynamicSize.GetComponent<SizeFitter>();

			if (sizeFitter)
			{
				sizeFitter.AdjustSize(true);
			}

			return dynamicSize.rect.height * TranslateVector.normalized;
		}
	}
}