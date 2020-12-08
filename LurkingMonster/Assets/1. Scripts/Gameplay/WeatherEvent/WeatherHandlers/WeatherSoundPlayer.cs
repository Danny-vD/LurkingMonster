using Audio;
using Enums.Audio;
using FMOD.Studio;
using ScriptableObjects;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	public class WeatherSoundPlayer : AbstractWeatherHandler
	{
		private EventInstance rainWind; 
		private EventInstance thunder;

		protected override void Start()
		{
			if (!AudioManager.IsInitialized)
			{
				return;
			}
			
			rainWind = AudioPlayer.GetEventInstance(EventType.RainWind);
			thunder  = AudioPlayer.GetEventInstance(EventType.Thunder);
			
			base.Start();
		}

		protected override void OnHeavyRainStart(WeatherEventData weatherData)
		{
			PlayThunderAndWind();
		}

		protected override void OnStormStart(WeatherEventData weatherData)
		{
			PlayThunderAndWind();
		}

		protected override void SetToDefault()
		{
			StopPlaying();
		}

		private void PlayThunderAndWind()
		{
			rainWind.start();
			thunder.start();
		}

		private void StopPlaying()
		{
			rainWind.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			thunder.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}

		protected override void OnDestroy()
		{
			// Release the FMOD events
			rainWind.release();
			thunder.release();
			
			base.OnDestroy();
		}
	}
}