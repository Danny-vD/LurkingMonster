using Gameplay;
using ScriptableObjects;
using VDFramework.EventSystem;

namespace Events
{
	public class RandomWeatherEvent : VDEvent
	{
		public readonly WeatherEventManager weatherEventManager;

		public RandomWeatherEvent(WeatherEventManager weatherEventManager)
		{
			this.weatherEventManager = weatherEventManager;
		}
	}
}