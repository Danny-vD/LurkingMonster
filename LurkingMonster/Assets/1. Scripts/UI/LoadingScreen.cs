using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VDFramework;

namespace UI
{
	public class LoadingScreen : BetterMonoBehaviour
	{
		private static AsyncOperation progress;
		private static LoadingScreen instance;

		[SerializeField]
		private float loadingSeconds = 2.0f;

		[SerializeField]
		private Slider progressSlider;

		private GameObject loadingScreen;

		private float loadingTime = 0.0f;

		private void Awake()
		{
			DontDestroyOnLoad(CachedGameObject);

			loadingScreen = CachedTransform.GetChild(0).gameObject;
			instance      = this;

			HideLoadingScreen();
		}

		/// <summary>
		/// Loads the scene with the given buildIndex
		/// </summary>
		public static void LoadScene(int buildIndex)
		{
			instance.ShowLoadingScreen();

			progress = SceneManager.LoadSceneAsync(buildIndex);
			instance.StartCoroutine(instance.Load());
		}

		/// <summary>
		/// Loads the scene with the given Name
		/// </summary>
		public static void LoadScene(string sceneName)
		{
			instance.ShowLoadingScreen();

			progress = SceneManager.LoadSceneAsync(sceneName);
			instance.StartCoroutine(instance.Load());
		}

		private IEnumerator Load()
		{
			progress.allowSceneActivation = false;

			// Give Unity time to enable the loading screen first
			yield return null;

			while (!progress.isDone)
			{
				progressSlider.value = progress.progress;

				EnforceMinimumLoadingTime();

				yield return null;
			}

			HideLoadingScreen();
		}

		private void EnforceMinimumLoadingTime()
		{
			loadingTime += Time.unscaledDeltaTime;

			if (loadingTime >= loadingSeconds)
			{
				progress.allowSceneActivation = true;
			}
		}

		private void ShowLoadingScreen()
		{
			loadingScreen.SetActive(true);
			loadingTime = 0.0f;
		}

		private void HideLoadingScreen()
		{
			loadingScreen.SetActive(false);
		}
	}
}