using System.Collections;
using System.Collections.Generic;
using Gameplay.Achievements;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

public class ProgressImageHandler : BetterMonoBehaviour
{
	[SerializeField]
	private Transform parent = null;

	[SerializeField]
	private GameObject prefabImage = null;
	
	[SerializeField]
	private Color unlockedColor;
	
	[SerializeField]
	private Color lockedColor;

	public void Instantiate(Achievement achievement)
	{
		for (int i = 0; i < achievement.Unlocked.Length; i++)
		{
			GameObject gameObject = Instantiate(prefabImage, parent);

			gameObject.GetComponent<Image>().color = achievement.Unlocked[i] ? unlockedColor : lockedColor;
		}
	}
}
