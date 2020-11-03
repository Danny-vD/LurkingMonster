using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VDFramework;

namespace UI.Layout
{
	[RequireComponent(typeof(RectTransform))]
	public class SizeFitter : BetterMonoBehaviour
	{
		[SerializeField, Tooltip("Extra space that will added to the size")]
		private float padding;

		[SerializeField, Tooltip("Space between children")]
		private float spacing;

		private RectTransform rectTransform;

		private IEnumerable<RectTransform> children;

		private void Awake()
		{
			rectTransform = (RectTransform) CachedTransform;

			children = rectTransform.Cast<RectTransform>();
		}

		private void Update()
		{
			AdjustSize();
		}

		private void AdjustSize()
		{
			// For every child that is active, add the rect height + spacing
			float newSize = children.Where(child => child.gameObject.activeSelf).Sum(child => child.rect.height + spacing);

			newSize += padding;

			rectTransform.sizeDelta = new Vector2(0, newSize);
		}
	}
}