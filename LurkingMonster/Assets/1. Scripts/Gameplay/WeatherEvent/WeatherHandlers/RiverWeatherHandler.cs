using System;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	public class RiverWeatherHandler : AbstractWeatherHandler
	{
		private static readonly int waveSpeed = Shader.PropertyToID("_WaveSpeed");
		private static readonly int waveLength = Shader.PropertyToID("_WaveLength");
		private static readonly int waveHeight = Shader.PropertyToID("_WaveHeight");
		private static readonly int color = Shader.PropertyToID("_Color");

		[SerializeField]
		private Material waterMaterial;

		[Header("Material Settings"), SerializeField]
		private MaterialSettings normal;

		[SerializeField]
		private MaterialSettings earthQuake;

		[SerializeField]
		private MaterialSettings rainfall;

		[SerializeField]
		private MaterialSettings storm;

		protected override bool AddWeatherListener => false;

		protected override void OnEarthQuakeStart(WeatherEventData weatherData)
		{
			SetMaterialSettings(earthQuake);
		}

		protected override void OnHeavyRainStart(WeatherEventData weatherData)
		{
			SetMaterialSettings(rainfall);
		}

		protected override void OnStormStart(WeatherEventData weatherData)
		{
			SetMaterialSettings(storm);
		}

		[ContextMenu("Set Normal")]
		protected override void SetToDefault()
		{
			SetMaterialSettings(normal);
		}
		
		private void SetMaterialSettings(MaterialSettings settings)
		{
			waterMaterial.SetFloat(waveHeight, settings.amplitude);
			waterMaterial.SetFloat(waveLength, settings.frequency);

			waterMaterial.SetFloat(waveSpeed, settings.speed);

			waterMaterial.SetColor(color, settings.waterColor);
		}

		[Serializable]
		private struct MaterialSettings
		{
			public float amplitude;
			public float frequency;

			public float speed;

			public Color waterColor;
		}
	}
}