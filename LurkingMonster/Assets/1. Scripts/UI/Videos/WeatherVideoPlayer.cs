using Events.WeatherEvents;

namespace UI.Videos
{
	public class WeatherVideoPlayer : AbstractVideoPlayerOnEvent<StartWeatherEvent>
	{
		protected override void OnVideoEnd()
		{
			CachedGameObject.SetActive(false);
		}
	}
}