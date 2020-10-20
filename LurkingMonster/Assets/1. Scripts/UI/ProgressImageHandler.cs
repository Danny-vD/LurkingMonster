using Gameplay.Achievements;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI
{
	public class ProgressImageHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private Transform parent = null;

		[SerializeField]
		private GameObject prefabImage = null;
	
		[SerializeField]
		private Color unlockedColor = Color.green;
	
		[SerializeField]
		private Color lockedColor = Color.red;

		public void Instantiate(Achievement achievement)
		{
			for (int i = 0; i < achievement.Unlocked.Length; i++)
			{
				GameObject gameObject = Instantiate(prefabImage, parent);

				gameObject.GetComponent<Image>().color = achievement.Unlocked[i] ? unlockedColor : lockedColor;
			}
		}
	}
}
