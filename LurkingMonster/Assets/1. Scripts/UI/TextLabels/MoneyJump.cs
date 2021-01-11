using Events.MoneyManagement;
using FMOD;
using TMPro;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class MoneyJump : BetterMonoBehaviour
	{
		private TextMeshProUGUI moneyText;
		private Color textColor; //Change the alpha to fade out
		
		private float disappearTimer = 0.8f;
		private float fadeOutSpeed = 1.5f;
		private float moveSpeed = 12f;

		
		public void SetUp(int rent)
		{
			moneyText = GetComponent<TextMeshProUGUI>();
			textColor = moneyText.color;
			moneyText.SetText(rent.ToString());
		}

		private void LateUpdate()
		{
			transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
			
			disappearTimer     -= Time.deltaTime;
			
			if (disappearTimer <= 0f)
			{
				textColor.a     -= fadeOutSpeed * Time.deltaTime;
				moneyText.color =  textColor;

				if (textColor.a < 0f)
				{
					Destroy(gameObject);
				}
			}
		}
	}
}