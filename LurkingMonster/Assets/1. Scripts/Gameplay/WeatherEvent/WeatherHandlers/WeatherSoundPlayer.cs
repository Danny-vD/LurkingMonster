using Audio;
using Enums;
using Enums.Audio;
using FMOD.Studio;
using FMODUnity;
using ScriptableObjects;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	public class WeatherSoundPlayer : AbstractWeatherHandler
	{
		private StudioEventEmitter backgroundEmitter;
		
		private EventInstance wind;
		private EventInstance rain;
		private EventInstance rainWind;
		private EventInstance earthQuake;

		protected override bool AddWeatherListener => true;

		protected override void Start()
		{
			if (!AudioManager.IsInitialized)
			{
				return;
			}

			wind       = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_WindBlowing);
			rain       = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_HeavyRain);
			rainWind   = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_RainAndWind);
			earthQuake = AudioPlayer.GetEventInstance(EventType.SFX_DISASTER_Earthquake);

			backgroundEmitter = AudioManager.Instance.EventPaths.GetEmitter(EmitterType.BackgroundMusic);

			base.Start();
		}

		protected override void StartWeather(WeatherEventType type, WeatherEventData data)
		{
			StopBackgroundMusic();
			base.StartWeather(type, data);
		}

		protected override void OnHeavyRainStart(WeatherEventData weatherData)
		{
			rain.start();
			wind.start();
		}

		protected override void OnHeavyRain(WeatherEventData weatherData)
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_DISASTER_Thunder);
		}

		protected override void OnStormStart(WeatherEventData weatherData)
		{
			rainWind.start();
		}

		protected override void OnStorm(WeatherEventData weatherData)
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_DISASTER_Thunder);
		}

		protected override void OnEarthQuakeStart(WeatherEventData weatherData)
		{
			earthQuake.start();
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
			wind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			rain.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			rainWind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			earthQuake.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

			backgroundEmitter.Event = AudioManager.Instance.EventPaths.GetPath(EventType.MUSIC_Background);
			backgroundEmitter.Play();
		}

		private void StopBackgroundMusic()
		{
			backgroundEmitter.Stop();
			backgroundEmitter.Event = string.Empty;
		}

		protected override void OnDestroy()
		{
			// Release the FMOD events
			wind.release();
			rain.release();
			rainWind.release();
			earthQuake.release();

			base.OnDestroy();
		}
	}
}