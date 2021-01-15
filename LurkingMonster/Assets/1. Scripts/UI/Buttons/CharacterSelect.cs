using Events;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class CharacterSelect : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject characterScreen;

		[SerializeField]
		private Image characterImage;
		
		private Button button;

		private Image[] characters;
		
		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(ShowCharacterSelectScreen);	
		}

		private void ShowCharacterSelectScreen()
		{
			characterScreen.SetActive(true);
			characters = characterScreen.GetComponentsInChildren<Image>();
			
			foreach (Image character in characters)
			{
				if (character.transform.childCount > 0)
				{
					Image childImage = character.transform.GetChild(0).GetComponent<Image>();

					if (childImage == null)
					{
						continue;
					}
					
					character.GetComponent<Button>().onClick.AddListener(OnCharacterClick);

					void OnCharacterClick()
					{
						Image tempImage = childImage;
						EventManager.Instance.RaiseEvent(new CharacterSelectEvent(tempImage));
						characterScreen.SetActive(false);
					}
				}
			}
		}
	}
}