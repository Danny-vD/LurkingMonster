using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class ClickImagineSwapper : BetterMonoBehaviour
	{
		[SerializeField]
		private Image image;

		[SerializeField]
		private List<Sprite> sprites = new List<Sprite>();

		private int index = 0;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(GoToNextSprite);
		}

		public void GoToNextSprite()
		{
			image.sprite = sprites[index++];

			index %= sprites.Count;
		}
	}
}