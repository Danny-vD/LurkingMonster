using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace Utility
{
	public class MaterialScroller : BetterMonoBehaviour
	{
		private static readonly int mainTex = Shader.PropertyToID("_MainTex");

		[SerializeField, Tooltip("The speed it needs to move per second")]
		private Vector2 speed = new Vector2(1, 1);

		[SerializeField]
		private bool IsUITexture = true;

		private Material currentMaterial;
		private Vector2 currentOffset;

		private void Awake()
		{
			currentMaterial = IsUITexture ? GetComponent<Image>().materialForRendering : GetComponent<Renderer>().sharedMaterial;
		}

		private void Update()
		{
			MoveOffset();
		}

		private void MoveOffset()
		{
			currentOffset += speed * Time.deltaTime;

			currentMaterial.SetTextureOffset(mainTex, currentOffset);
		}
	}
}