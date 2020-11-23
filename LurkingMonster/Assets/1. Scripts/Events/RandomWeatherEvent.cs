using Gameplay;
using Gameplay.WeatherEvent;
using ScriptableObjects;
using VDFramework.EventSystem;

namespace Events
{
	public class RandomWeatherEvent : VDEvent
	{
		public readonly AbstractWeatherEvent AbstractWeatherEvent;

		public RandomWeatherEvent(AbstractWeatherEvent abstractWeatherEvent)
		{
			this.AbstractWeatherEvent = abstractWeatherEvent;
		}
	}
}