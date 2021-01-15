using Singletons;
using System;
using Audio;
using Enums.Audio;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Sliders
{
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
			float currentVolume = 1f;
			
			switch (busType)
			{
				case BusType.Master:
					//currentVolume = UserSettings.GameData.MasterVolume;
					print("Master volume is handled by the device itself");
					break;
				case BusType.SFX:
					currentVolume = UserSettings.GameData.SFXVolume;
					break;
				case BusType.Music:
					currentVolume = UserSettings.GameData.MusicVolume;
					break;
				case BusType.Ambient:
					currentVolume = UserSettings.GameData.AmbientVolume;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			volumeSlider.value = currentVolume;
		}
		
		private void SetVolume(float volume)
		{
			AudioManager.Instance.SetVolume(busType, volume);

			StoreVolume(volume);
		}

		private void StoreVolume(float volume)
		{
			switch (busType)
			{
				case BusType.Master:
					//UserSettings.GameData.MasterVolume = volume;
					print("Master volume is handled by the device itself");
					break;
				case BusType.SFX:
					UserSettings.GameData.SFXVolume = volume;
					break;
				case BusType.Music:
				case BusType.Ambient:
					UserSettings.GameData.AmbientVolume = volume;
					UserSettings.GameData.MusicVolume   = volume;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}