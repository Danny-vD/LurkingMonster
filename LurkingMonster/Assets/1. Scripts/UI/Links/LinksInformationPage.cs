using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using VDFramework;

namespace UI.Links
{
	[RequireComponent(typeof(TMP_Text))]
	public class LinksInformationPage : BetterMonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			TMP_Text link = GetComponentInChildren<TMP_Text>();
			int linkIndex = TMP_TextUtilities.FindIntersectingLink(link, Input.mousePosition, null);

			if (linkIndex != -1)
			{
				TMP_LinkInfo linkInfo = link.textInfo.linkInfo[linkIndex];
				
				Application.OpenURL(linkInfo.GetLinkID());
			}
		}
	}
}