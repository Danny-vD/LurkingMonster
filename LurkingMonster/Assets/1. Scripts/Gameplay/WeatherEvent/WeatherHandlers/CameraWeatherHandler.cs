using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	public class CameraWeatherHandler : AbstractWeatherHandler
	{
		/// <summary>
		/// tau is a mathematical constant that equals 2 * PI
		/// </summary>
		private const float tau = 6.2831853071795862f;
		
		[SerializeField]
		private float Ehorizontal, eVertical, eFrequency;

		[SerializeField]
		private float sHorizontal, sVertical, sFrequency;

		protected override bool AddWeatherListener => true;

		protected override void OnEarthQuake(WeatherEventData data)
		{
			CameraMovement(Ehorizontal, eVertical, eFrequency, data.interval);
		}

		protected override void OnStorm(WeatherEventData data)
		{
			CameraMovement(sHorizontal, sVertical, sFrequency, data.interval);
		}

		protected override void SetToDefault()
		{
			StopAllCoroutines();
		}
		
		private void CameraMovement(float horizontal, float vertical, float frequency, float time)
		{
			StopAllCoroutines();
			StartCoroutine(Shake(time, horizontal, vertical, frequency * tau));
		}

		private IEnumerator Shake(float time, float horizontal, float vertical, float frequency)
		{
			while (time > 0)
			{
				time -= Time.deltaTime;
				Vector3 test = new Vector3(
					Mathf.Sin(Time.realtimeSinceStartup * frequency) * horizontal,
					Mathf.Sin(Time.realtimeSinceStartup * frequency / 4 - 0.5f) * vertical / 8,
					0);
				CachedTransform.Translate(test, Space.Self);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}