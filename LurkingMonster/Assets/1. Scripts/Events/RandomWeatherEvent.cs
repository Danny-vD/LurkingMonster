using ScriptableObjects;
using VDFramework.EventSystem;

namespace Events
{
	public class RandomWeatherEvent : VDEvent
	{
		public readonly WeatherEventData WeatherEventData;

		public RandomWeatherEvent(WeatherEventData weatherEventData)
		{
			WeatherEventData = weatherEventData;
		}
	}
}