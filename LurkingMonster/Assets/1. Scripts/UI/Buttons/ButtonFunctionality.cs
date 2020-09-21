﻿using Events;
using UnityEngine.SceneManagement;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
    public class ButtonFunctionality : BetterMonoBehaviour
    {
        public void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
			UnityEngine.Application.Quit();
#endif
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
	}
}
