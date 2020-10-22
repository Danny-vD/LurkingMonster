namespace UI.Sliders
{
	using System;
	using Audio;
	using Enums.Audio;
	using UnityEngine;
	using UnityEngine.UI;
	using VDFramework;

	[RequireComponent(typeof(Slider))]
	public class VolumeSlider : BetterMonoBehaviour
	{
		[SerializeField]
		private BusType busType = default;
		
		private Slider volumeSlider;

		private void Awake()
		{
			volumeSlider = GetComponent<Slider>();
			
			volumeSlider.onValueChanged.AddListener(SetVolume);
		}

		private void Start()
		{
			SetToCorrectVolume();
		}

		private void SetToCorrectVolume()
		{
			float currentVolume = 0.7f; // Not too loud
			
			switch (busType)
			{
				case BusType.Master:
					//currentVolume = UserSettings.GameData.MasterVolume;
					break;
				case BusType.SFX:
					break;
				case BusType.Music:
					break;
				case BusType.Ambient:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			volumeSlider.value = currentVolume;
		}
		
		private void SetVolume(float volume)
		{
			print(volume);
			
			AudioManager.Instance.SetVolume(busType, volume);

			StoreVolume(volume);
		}

		private void StoreVolume(float volume)
		{
			switch (busType)
			{
				case BusType.Master:
					// UserSettings.GameData.MasterVolume = volume
					break;
				case BusType.SFX:
					break;
				case BusType.Music:
					break;
				case BusType.Ambient:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}