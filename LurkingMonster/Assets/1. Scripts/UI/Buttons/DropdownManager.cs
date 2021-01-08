using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class DropdownManager : BetterMonoBehaviour
	{
		[SerializeField]
		private Button btnExit;

		private Button button;
		
		private bool active;

		private void Awake()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(() => { active = !active; });
			btnExit.onClick.AddListener(RetractDropdown);
		}

		public void ExtendDropdown()
		{
			if (!active)
			{
				button.onClick.Invoke();
			}
		}

		private void RetractDropdown()
		{
			if (active)
			{
				button.onClick.Invoke();
			}
		}
	}
}