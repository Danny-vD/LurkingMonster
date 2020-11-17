using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Utility
{
    [RequireComponent(typeof(ScrollRect))]
    public class InfiniteScroll : MonoBehaviour
    {
        private ScrollRect scrollRect;

        private void Awake()
        {
            //scrollRect.content.childCount;
            scrollRect = GetComponent<ScrollRect>();

            scrollRect.verticalNormalizedPosition = 0.5f;
        }
    }
}
