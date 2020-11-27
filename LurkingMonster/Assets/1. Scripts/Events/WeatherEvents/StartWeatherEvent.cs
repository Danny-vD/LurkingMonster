using Gameplay.WeatherEvent;
using VDFramework.EventSystem;

namespace Events.WeatherEvents
{
	public class StartWeatherEvent : VDEvent<StartWeatherEvent>
	{
		public readonly AbstractWeatherEvent AbstractWeatherEvent;

		public StartWeatherEvent(AbstractWeatherEvent abstractWeatherEvent)
		{
			AbstractWeatherEvent = abstractWeatherEvent;
		}
	}
}