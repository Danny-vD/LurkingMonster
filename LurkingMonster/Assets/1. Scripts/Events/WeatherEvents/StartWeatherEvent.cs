using Gameplay.WeatherEvent;
using VDFramework.EventSystem;

namespace Events.WeatherEvents
{
	public class StartWeatherEvent : VDEvent<StartWeatherEvent>
	{
		public readonly bool ShowAnimation;
		public readonly AbstractWeatherEvent AbstractWeatherEvent;

		public StartWeatherEvent(AbstractWeatherEvent abstractWeatherEvent, bool showAnimation = true)
		{
			AbstractWeatherEvent = abstractWeatherEvent;
			ShowAnimation   = showAnimation;
		}
	}
}