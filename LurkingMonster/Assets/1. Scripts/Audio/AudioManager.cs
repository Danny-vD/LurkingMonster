using System;
using System.Collections.Generic;
using Structs.Audio;
using Enums.Audio;
using IO;
using Singletons;
using Utility;
using VDFramework.Singleton;
using EventType = Enums.Audio.EventType;

namespace Audio
{
	public class AudioManager : Singleton<AudioManager>
	{
		public EventPaths EventPaths;

		public List<InitialValuePerBus> initialVolumes = new List<InitialValuePerBus>();

		protected override void Awake()
		{
			if (EventPaths == null)
			{
				throw new Exception("Audiomanager should not be initialized through code, it needs to be present in the scene already.");
			}

			base.Awake();
			EventPaths.AddEmitters(gameObject);

			DontDestroyOnLoad(gameObject);

			SetInitialVolumes();
			AudioPlayer.PlayEmitter(EmitterType.BackgroundMusic);
		}

		private void Start()
		{
			if (UserSettings.SettingsExist)
			{
				LoadVolumes();
			}
		}

		private static void LoadVolumes()
		{
			GameData gameData = UserSettings.GameData;
			
			Instance.SetVolume(BusType.Music, gameData.MusicVolume);
			Instance.SetVolume(BusType.Ambient, gameData.AmbientVolume);
		}

		private void SetInitialVolumes()
		{
			foreach (InitialValuePerBus pair in initialVolumes)
			{
				if (pair.Key == BusType.Master)
				{
					AudioParameterManager.SetMasterVolume(pair.Value);
					AudioParameterManager.SetMasterMute(pair.isMuted);
					continue;
				}

				string busPath = EventPaths.GetPath(pair.Key);
				AudioParameterManager.SetBusVolume(busPath, pair.Value);
				AudioParameterManager.SetBusMute(busPath, pair.isMuted);
			}
		}

		public void SetVolume(BusType busType, float volume)
		{
			AudioParameterManager.SetBusVolume(EventPaths.GetPath(busType), volume);
		}

		public float GetVolume(BusType busType)
		{
			return AudioParameterManager.GetBusVolume(EventPaths.GetPath(busType));
		}
	}
}