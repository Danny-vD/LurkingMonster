using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class ChangeButtonSprite : BetterMonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField]
		private Sprite btnDown;

		[SerializeField]
		private Sprite btnUp;
		
		private Button button;

		private void Start()
		{
			button              = GetComponent<Button>();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			button.image.sprite = btnDown;
			button.transform.Translate(Vector3.down * 8f);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			button.image.sprite = btnUp;
			button.transform.Translate(Vector3.up * 8f);
		}
	}
}