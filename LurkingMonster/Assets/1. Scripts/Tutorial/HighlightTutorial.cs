using UnityEngine;
using UnityEngine.UI;

namespace _1._Scripts.Tutorial
{
	public class HighlightTutorial : Tutorial
	{
		[SerializeField]
		private GameObject highlight;

		[SerializeField]
		private Button button;

		[SerializeField]
		private float scale;

		[SerializeField]
		private float offsetDistance = 250f;
		
		private GameObject prefabInstance;

		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			base.StartTutorial(narrator, arrow);
			ShowNextText();

			SetNextButton(Highlight);
			button.onClick.AddListener(CompleteTutorial);
		}

		private void Highlight()
		{
			ShowNextText();
			prefabInstance = Instantiate(arrow, highlight.transform, true);

			prefabInstance.transform.localPosition = Vector3.zero;
			prefabInstance.transform.localScale = new Vector3(scale, scale, scale);
			prefabInstance.transform.Translate(Vector3.up * offsetDistance);
		}

		private void CompleteTutorial()
		{
			Destroy(prefabInstance);
			TutorialManager.Instance.CompletedTutorial();
		}
	}
}