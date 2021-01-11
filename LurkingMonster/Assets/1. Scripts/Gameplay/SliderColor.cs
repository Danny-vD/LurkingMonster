using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace Gameplay
{
	[RequireComponent(typeof(Slider))]
	public class SliderColor : BetterMonoBehaviour
	{
		[Header("The color to switch to when the slider is below or equal to a certain percentage")]
		[SerializeField]
		private SerializableDictionary<float, Color> ColorPerPercentage;
		
		[SerializeField]
		private Image fillarea;

		private Slider slider;

		private List<float> steps;

		private void Awake()
		{
			slider = GetComponent<Slider>();
			
			slider.onValueChanged.AddListener(ChangeColor);
			
			steps = ColorPerPercentage.Select(pair => pair.Key).ToList();
			steps.Sort();
		}

		private void OnEnable()
		{
			ChangeColor(slider.value);
		}
		
		private void Start()
		{
			ChangeColor(slider.value);
		}

		private void ChangeColor(float sliderValue)
		{
			float normalizedValue = Mathf.InverseLerp(slider.minValue, slider.maxValue, sliderValue);
			
			for (int i = steps.Count - 1; i >= 0; i--)
			{
				if (normalizedValue <= steps[i])
				{
					fillarea.color = ColorPerPercentage[steps[i]];
				}
			}
		}
	}
}