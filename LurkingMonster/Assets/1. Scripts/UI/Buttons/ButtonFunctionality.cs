﻿using System.IO;
using Events;
using UnityEngine.SceneManagement;
using VDFramework;
using VDFramework.EventSystem;
using UnityEngine;
using UnityEngine.UI;



namespace UI
{
    using Audio;
    using Enums.Audio;
	
    public class ButtonFunctionality : BetterMonoBehaviour
    {
		[SerializeField]
		public Text textDeleteFile;
		
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
