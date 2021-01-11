using Audio;
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

		protected override bool AddWeatherListener => false;

		protected override void Start()
		{
			if (!AudioManager.IsInitialized)
			{
				return;
			}
			
			rain = AudioPlayer.GetEventInstance(EventType.HeavyRain);
			rainWind = AudioPlayer.GetEventInstance(EventType.RainAndWind);
			wind = AudioPlayer.GetEventInstance(EventType.WindBlowing);
			
			
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