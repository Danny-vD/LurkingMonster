﻿using UnityEngine;
using UnityEngine.UI;

namespace Tutorials
{
	public class HighlightTutorial : Tutorial
	{
		[SerializeField]
		private GameObject highlight;

		[SerializeField]
		private Button continueButton;

		[SerializeField]
		private float scale;

		[SerializeField]
		private float offsetDistance = 250f;

		private GameObject prefabInstance;

		public override void StartTutorial(GameObject arrow, TutorialManager manager)
		{
			base.StartTutorial(arrow, manager);

			if (keyCount == 0)
			{
				Highlight();
			}
			else
			{
				ShowNextText();
			}

			DisableButtons();

			SetNextButton(Highlight);
			continueButton.onClick.AddListener(CompleteTutorial);
		}

		private void DisableButtons()
		{
			Button[] buttons = FindObjectsOfType<Button>();

			foreach (Button disableButton in buttons)
			{
				if (continueButton == disableButton || disableButton == manager.StopTutorialButton)
				{
					continue;
				}

				disableButton.onClick.RemoveAllListeners();
			}
		}

		private void Highlight()
		{
			ShowNextText();
			prefabInstance = Instantiate(Arrow, highlight.transform, true);

			prefabInstance.transform.localPosition = Vector3.zero;

			prefabInstance.transform.localScale = new Vector3(scale, scale, scale);

			prefabInstance.transform.Translate(Vector3.up * offsetDistance);
		}

		private void CompleteTutorial()
		{
			Destroy(prefabInstance);
			manager.CompletedTutorial();
		}
	}
}