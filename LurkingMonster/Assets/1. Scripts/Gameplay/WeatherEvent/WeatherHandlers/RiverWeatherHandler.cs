using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utility;

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
		private SerializableEnumDictionary<WeatherEventType, MaterialSettings> weatherSettings;

		protected override bool AddWeatherListener => false;

		protected override void StartWeather(WeatherEventType type, WeatherEventData data)
		{
			SetMaterialSettings(weatherSettings[type]);
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