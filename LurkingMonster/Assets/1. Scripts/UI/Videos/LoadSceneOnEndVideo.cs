using Enums;
using Events;
using UI.Buttons;
using UnityEngine;
using UnityEngine.Video;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Videos
{
	[RequireComponent(typeof(VideoPlayer))]
	public class LoadSceneOnEndVideo : BetterMonoBehaviour
	{
		[SerializeField]
		private int sceneIndex;

		[SerializeField]
		private SerializableEnumDictionary<Language, VideoClip> videoClips;
		
		private bool playingVideo;

		private VideoPlayer videoPlayer;

		private void Awake()
		{
			videoPlayer = GetComponent<VideoPlayer>();
		}

		private void Start()
		{
			LanguageChangedEvent.ParameterlessListeners += ChangedLanguage;
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			LanguageChangedEvent.ParameterlessListeners -= ChangedLanguage;
		}

		private void ChangedLanguage()
		{
			videoPlayer.clip = videoClips[LanguageSettings.Language];
		}

		public void PlayVideo()
		{
			playingVideo = true;
			videoPlayer.Play();
		}

		private void Update()
		{
			if (!playingVideo)
			{
				return;
			}
			
			if (ReachedEnd() || Input.anyKey)
			{
				LoadingScreen.LoadScene(sceneIndex);
				Destroy(CachedGameObject);
			}
		}

		private bool ReachedEnd()
		{
			return videoPlayer.frame > 0 && videoPlayer.isPlaying == false;
		}
	}
}