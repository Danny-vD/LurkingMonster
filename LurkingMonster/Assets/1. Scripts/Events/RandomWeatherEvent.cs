using Gameplay;
using Gameplay.WeatherEvent;
using ScriptableObjects;
using VDFramework.EventSystem;

namespace Events
{
	public class RandomWeatherEvent : VDEvent<RandomWeatherEvent>
	{
		public readonly AbstractWeatherEvent AbstractWeatherEvent;

		public RandomWeatherEvent(AbstractWeatherEvent abstractWeatherEvent)
		{
			AbstractWeatherEvent = abstractWeatherEvent;
		}
	}
}