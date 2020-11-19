using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	[RequireComponent(typeof(Button))]
	public class ClickImageSwapper : BetterMonoBehaviour
	{
		[SerializeField]
		private Image image = null;

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