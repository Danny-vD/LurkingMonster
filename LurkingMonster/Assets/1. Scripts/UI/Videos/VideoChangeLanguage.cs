using System;
using Enums;
using Events;
using UnityEngine;
using UnityEngine.Video;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Videos
{
	[RequireComponent(typeof(VideoPlayer))]
	public class VideoChangeLanguage : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableEnumDictionary<Language, VideoClip> videoClips;
		
		private VideoPlayer videoPlayer;

		private void Awake()
		{
			videoPlayer = GetComponent<VideoPlayer>();
		}

		private void Start()
		{
			LanguageChangedEvent.ParameterlessListeners += SetVideoLanguage;
			SetVideoLanguage();
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			LanguageChangedEvent.ParameterlessListeners -= SetVideoLanguage;
		}

		private void SetVideoLanguage()
		{
			videoPlayer.clip = videoClips[LanguageSettings.Language];
		}
	}
}