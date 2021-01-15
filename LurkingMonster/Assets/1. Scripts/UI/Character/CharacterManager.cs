using System.Collections.Generic;
using Events;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Character
{
	public class CharacterManager : BetterMonoBehaviour
	{
		[SerializeField]
		private List<Sprite> characters;

		private Image targetImage;

		private void Start()
		{
			targetImage = GetComponent<Image>();
			EventManager.Instance.AddListener<CharacterSelectEvent>(UpdateCharacter);

			if (UserSettings.SettingsExist)
			{
				OnLoad();
			}
		}

		private void UpdateCharacter(CharacterSelectEvent characterEvent)
		{
			Sprite sprite = characterEvent.character.sprite;
			targetImage.sprite = sprite;
			int index = characters.IndexOf(sprite);

			UserSettings.GameData.CharacterIndex = index;
		}

		private void OnLoad()
		{
			targetImage.sprite = characters[UserSettings.GameData.CharacterIndex];
		}
	}
}