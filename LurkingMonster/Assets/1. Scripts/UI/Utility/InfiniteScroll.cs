using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Utility
{
	[RequireComponent(typeof(ScrollRect))]
	public class InfiniteScroll : BetterMonoBehaviour
	{
		[SerializeField]
		private RectTransform content;

		private ScrollRect scrollRect;

		private int originalChildCount;
		private int childCount;
		private float step;

		private void Awake()
		{
			scrollRect = GetComponent<ScrollRect>();

			DuplicateChildren();
			CalculateData();
			
			SetScrollViewToChild(4);
		}

		private void DuplicateChildren()
		{
			IEnumerable<RectTransform> children = content.Cast<RectTransform>().ToArray();

			originalChildCount = content.childCount;
			
			// Duplicate all children twice, so that there is a clear 'middle' to set the start scroll to
			for (int i = 0; i < 2; i++)
			{
				foreach (RectTransform child in children)
				{
					Instantiate(child, content);
				}
			}

			childCount = originalChildCount * 3;
		}

		private void CalculateData()
		{
			step = 1.0f / childCount;
		}

		private void SetScrollViewToChild(int childIndex)
		{
			scrollRect.verticalNormalizedPosition = 1 - childIndex * step;
		}
	}
}