using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VDFramework;

namespace Utility
{
	public class MaterialScroller : BetterMonoBehaviour
	{
		private static readonly int mainTex = Shader.PropertyToID("_MainTex");

		[SerializeField, Tooltip("The speed it needs to move per second")]
		private Vector2 speed = new Vector2(1, 0);

		[SerializeField, Tooltip("Is the material that we scroll on an UI element or is it on an object in the game?")]
		private bool IsUITexture = true;

		[SerializeField]
		private bool unscaledTime;

		private Material currentMaterial;
		private Vector2 currentOffset;

		private RawImage rawImage;

		private Action scrollMethod = null;

		private void Awake()
		{
			if (IsUITexture)
			{
				rawImage = GetComponent<RawImage>();

				if (rawImage)
				{
					scrollMethod = ScrollRawImage;
					return;
				}

				currentMaterial = GetComponent<Image>().materialForRendering;
			}
			else
			{
				currentMaterial = GetComponent<Renderer>().sharedMaterial;
			}

			scrollMethod = ScrollMaterial;
		}

		private void Update()
		{
			scrollMethod();
		}

		private void ScrollMaterial()
		{
			float deltaTime = unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

			currentOffset += speed * deltaTime;

			currentMaterial.SetTextureOffset(mainTex, currentOffset);
		}

		private void ScrollRawImage()
		{
			float deltaTime = unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

			Rect uvRect = rawImage.uvRect;
			Vector2 scroll = new Vector2(uvRect.x, uvRect.y) + speed * deltaTime;
			uvRect.Set(scroll.x, scroll.y, uvRect.width, uvRect.height);

			rawImage.uvRect = uvRect;
		}
	}
}