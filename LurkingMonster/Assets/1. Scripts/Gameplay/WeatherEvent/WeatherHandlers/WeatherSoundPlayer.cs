﻿using Audio;
using Enums.Audio;
using FMOD.Studio;
using ScriptableObjects;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	public class WeatherSoundPlayer : AbstractWeatherHandler
	{
		private EventInstance rainWind; 
		private EventInstance wind;
		private EventInstance rain;

		protected override bool AddWeatherListener => true;

		protected override void Start()
		{
			if (!AudioManager.IsInitialized)
			{
				return;
			}
			
			rain = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_HeavyRain);
			rainWind = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_RainAndWind);
			wind = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_WindBlowing);

			base.Start();
		}

		protected override void OnHeavyRainStart(WeatherEventData weatherData)
		{
			rain.start();
			wind.start();
		}

		protected override void OnStormStart(WeatherEventData weatherData)
		{
			rainWind.start();
		}

		protected override void OnEarthQuakeStart(WeatherEventData weatherData)
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_DISASTER_Earthquake);
		}

		protected override void OnEarthQuake(WeatherEventData weatherData)
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_DISASTER_EARTHQUAKE_Quake);
		}

		protected override void SetToDefault()
		{
			StopPlaying();
		}

		private void StopPlaying()
		{
			rainWind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			wind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			rain.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}

		protected override void OnDestroy()
		{
			// Release the FMOD events
			rainWind.release();
			wind.release();
			
			base.OnDestroy();
		}
	}
}