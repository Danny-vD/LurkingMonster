using UnityEngine;
using UnityEngine.Video;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Videos
{
	[RequireComponent(typeof(VideoPlayer))]
	public abstract class AbstractVideoPlayerOnEvent<TEvent> : BetterMonoBehaviour
		where TEvent : VDEvent
	{
		[SerializeField]
		protected GameObject RenderTexture;
		
		protected VideoPlayer VideoPlayer;
		private bool playingVideo = false;

		protected virtual void Awake()
		{
			VideoPlayer = GetComponent<VideoPlayer>();
		}

		protected virtual void Start()
		{
			EventManager.Instance.AddListener<TEvent>(StartVideo);
		}

		private void Update()
		{
			if (!playingVideo)
			{
				return;
			}

			if (ReachedEnd())
			{
				OnVideoEnd();
				playingVideo = false;
			}
		}

		protected virtual void OnDestroy()
		{
			EventManager.Instance.RemoveListener<TEvent>(StartVideo);
		}

		/// <summary>
		/// Starts the video
		/// </summary>
		public virtual void StartVideo()
		{
			RenderTexture.SetActive(true);
			CachedGameObject.SetActive(true);
			playingVideo = true;
			VideoPlayer.Play();
		}

		/// <summary>
		/// A function that will be called when the video ends
		/// </summary>
		protected abstract void OnVideoEnd();

		private bool ReachedEnd()
		{
			return VideoPlayer.frame > 0 && VideoPlayer.isPlaying == false;
		}
	}
}