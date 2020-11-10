using System;
using CameraScripts;
using UnityEngine;
using VDFramework;

namespace Utility
{
	public class ZoomScaler : BetterMonoBehaviour
	{
		[SerializeField]
		private Vector2 MinMaxZoom = new Vector2(0.5f, 2.0f);
		
		private CameraZoom zoom;

		private float lastScale = 0;
		private float scale = 1;

		private void Start()
		{
			zoom = Camera.main.GetComponent<CameraZoom>();
		}

		private void Update()
		{
			float zoomFactor = zoom.CalculateNormalizedZoom();

			scale = Mathf.Lerp(MinMaxZoom.y, MinMaxZoom.x, zoomFactor);
			

			if (!Mathf.Approximately(scale, lastScale))
			{
				CachedTransform.localScale = new Vector3(scale, scale, scale);

				lastScale = scale;
			}
		}
	}
}