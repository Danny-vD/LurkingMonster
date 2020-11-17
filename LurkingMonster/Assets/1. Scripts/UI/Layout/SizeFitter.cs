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
			AdjustSize(false);
		}

		public void AdjustSize(bool ignoreDisabledChildren)
		{
			if (CachedTransform.childCount == 0)
			{
				return;
			}

			IEnumerable<RectTransform> includedChildren = ignoreDisabledChildren ? children : children.Where(child => child.gameObject.activeSelf);
			
			// For every child that is active, add the rect height + spacing
			float newSize = includedChildren.Sum(child => child.rect.height + spacing);
			newSize -= spacing; // No spacing needed for last child
			
			newSize += padding;

			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newSize);
		}
	}
}