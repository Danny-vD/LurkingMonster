﻿using System;
using Enums;
using Events;
using Gameplay.WeatherEvent;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Aesthetics
{
	public class RiverWeatherHandler : BetterMonoBehaviour
	{
		private static readonly int waveSpeed = Shader.PropertyToID("_WaveSpeed");
		private static readonly int waveLength = Shader.PropertyToID("_WaveLength");
		private static readonly int waveHeight = Shader.PropertyToID("_WaveHeight");
		private static readonly int color = Shader.PropertyToID("_Color");
		
		[Header("Material Settings"), SerializeField]
		private MaterialSettings normal;

		[SerializeField]
		private MaterialSettings earthQuake;
		
		[SerializeField]
		private MaterialSettings rainfall;
		
		[SerializeField]
		private MaterialSettings storm;

		private Material material;

		private AbstractWeatherEvent weatherEvent;
		private bool isModified;

		private void Awake()
		{
			material = GetComponent<Renderer>().sharedMaterial;
			SetMaterialSettings(normal);
			
			EventManager.Instance.AddListener<RandomWeatherEvent>(AddListener);
		}
		
		private void AddListener(RandomWeatherEvent randomWeatherEvent)
		{
			weatherEvent = randomWeatherEvent.AbstractWeatherEvent;

			switch (weatherEvent.WeatherType)
			{
				case WeatherEventType.Drought:
					weatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.HeavyRain:
					weatherEvent.RegisterListener(RainFallEffect);
					break;
				case WeatherEventType.Earthquake:
					weatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.Storm:
					weatherEvent.RegisterListener(StormEffects);
					break;
				case WeatherEventType.GasWinning:
					weatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.BuildingTunnels:
					weatherEvent.RegisterListener(EarthquakeEffects);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Update()
		{
			if (isModified && !WeatherEventManager.WeatherEventActive)
			{
				SetMaterialSettings(normal);
			}
		}

		private void EarthquakeEffects()
		{
			SetMaterialSettings(earthQuake);
		}

		private void RainFallEffect()
		{
			SetMaterialSettings(rainfall);
		}
		
		private void StormEffects()
		{
			SetMaterialSettings(storm);
		}

		private void SetMaterialSettings(MaterialSettings settings)
		{
			isModified = !settings.Equals(normal);
			
			material.SetFloat(waveHeight, settings.amplitude);
			material.SetFloat(waveLength, settings.frequency);
			
			material.SetFloat(waveSpeed, settings.speed);
			
			material.SetColor(color, settings.color);
		}

		[ContextMenu("Normal")]
		private void SetNormal()
		{
			material = GetComponent<Renderer>().sharedMaterial;
			SetMaterialSettings(normal);
		}
		
		[Serializable]
		private struct MaterialSettings
		{
			public float amplitude;
			public float frequency;
			
			public float speed;

			public Color color;
		}
	}
}
