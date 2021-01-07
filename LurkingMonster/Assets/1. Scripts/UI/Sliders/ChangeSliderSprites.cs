using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class ChangeSliderSprites : BetterMonoBehaviour
	{
		[SerializeField]
		private Sprite[] sprites = new Sprite[0];

		[SerializeField]
		private Slider slider;

		private Image image;

		private float[] steps;
		
		private void Start()
		{
			image = GetComponent<Image>();
			
			steps = new float[sprites.Length];
			CalculateSteps();
			
			ChangeSprites(slider.value);
			
			slider.onValueChanged.AddListener(ChangeSprites);
		}

		private void ChangeSprites(float sliderValue)
		{
			for (int i = steps.Length - 1; i >= 0; i--)
			{
				if (sliderValue <= steps[i])
				{
					image.sprite = sprites[i];
				}
			}
		}

		private void CalculateSteps()
		{
			float step = 1.0f / (steps.Length - 1);

			steps[0] = 0.0f;

			for (int i = 1; i < steps.Length; i++)
			{
				steps[i] = steps[i - 1] + step;
			}
		}
	}
}