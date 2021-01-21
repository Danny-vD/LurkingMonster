using UnityEngine;
using UnityEngine.Video;
using VDFramework;

namespace UI.Videos
{
	[RequireComponent(typeof(VideoPlayer))]
	public class LoadSceneOnEndVideo : BetterMonoBehaviour
	{
		[SerializeField]
		private int sceneIndex;

		private bool playingVideo;

		private VideoPlayer videoPlayer;

		private void Awake()
		{
			videoPlayer = GetComponent<VideoPlayer>();
		}

		public void PlayVideo()
		{
			playingVideo = true;
			videoPlayer.Play();
		}

		public void EndVideo()
		{
			LoadingScreen.LoadScene(sceneIndex);
			Destroy(CachedGameObject);
		}

		private void Update()
		{
			if (!playingVideo)
			{
				return;
			}
			
			if (ReachedEnd())
			{
				EndVideo();
			}
		}

		private bool ReachedEnd()
		{
			return videoPlayer.frame > 0 && videoPlayer.isPlaying == false;
		}
	}
}