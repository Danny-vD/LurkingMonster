using UnityEngine;

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
	}
}