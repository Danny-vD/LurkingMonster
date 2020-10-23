using Singletons;

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
		private BusType busType;
		
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
					print("Master volume is not in the game currently");
					break;
				case BusType.SFX:
					//currentVolume = UserSettings.GameData.SFX;
					print("SFX volume is not in the game currently");
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
					// UserSettings.GameData.MasterVolume = volume
					print("Master volume is not in the game currently");
					break;
				case BusType.SFX:
					// UserSettings.GameData.SFX = volume
					print("SFX volume is not in the game currently");
					break;
				case BusType.Music:
					UserSettings.GameData.MusicVolume = volume;
					break;
				case BusType.Ambient:
					UserSettings.GameData.AmbientVolume = volume;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}