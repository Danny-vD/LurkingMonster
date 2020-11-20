using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Weather Events/Weather Data")]
	public class WeatherEventData : ScriptableObject
	{
		[SerializeField]
		public float SoilTime;
		
		[SerializeField]
		public float FoundationTime;
		
		[SerializeField]
		public float BuildingTime;

		[SerializeField]
		public float Timer;

		[SerializeField]
		public string JsonEventKey;
		
		[SerializeField]
		public string JsonContentKey;

		[SerializeField]
		public float interval;
	}
}