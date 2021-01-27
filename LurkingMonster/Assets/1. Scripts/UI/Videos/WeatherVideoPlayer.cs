using Events.WeatherEvents;

namespace UI.Videos
{
	public class WeatherVideoPlayer : AbstractVideoPlayerOnEvent<StartWeatherEvent>
	{
		protected override void Start()
		{
			base.Start();
			VideoPlayer.Prepare();
		}

		public override void StartVideo(StartWeatherEvent @event)
		{
			if (@event.ShowAnimation)
			{
				base.StartVideo(@event);
			}
		}

		protected override void OnVideoEnd()
		{
			RenderTexture.SetActive(false);
		}
	}
}