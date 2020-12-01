using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class LoadingScreen : BetterMonoBehaviour
	{
		private static LoadingScreen instance;
		
		[SerializeField]
		private Slider progressSlider;
		
		private static AsyncOperation progress;

		private Transform loadingScreen;
		
		private void Awake()
		{
			loadingScreen  = CachedTransform.GetChild(0);
			instance       = this;
			DontDestroyOnLoad(gameObject);
			loadingScreen.gameObject.SetActive(false);
		}

		private IEnumerator Load()
		{
			while (!progress.isDone)
			{
				progressSlider.value = progress.progress;

				yield return null;
			}
			
			HideLoadingScreen();
		}

		// private static void AllowScene(bool allow)
		// {
		// 	progress.allowSceneActivation = allow;
		// }

		private void ShowLoadingScreen()
		{
			loadingScreen.gameObject.SetActive(true);
			StartCoroutine(Load());
		}

		private void HideLoadingScreen()
		{
			CachedGameObject.SetActive(false);
		}
		
		public static void LoadScene(int index)
		{
			progress = SceneManager.LoadSceneAsync(index);
			instance.ShowLoadingScreen();
		}
		
		public static void LoadScene(string scene)
		{
			progress = SceneManager.LoadSceneAsync(scene);
			instance.ShowLoadingScreen();
		}
	}
}