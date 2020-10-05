using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Singleton;

namespace Singletons
{
	public class MessageManager : Singleton<MessageManager>
	{
		[SerializeField]
		private Text message = null;

		public void ShowMessageGameUI(string inputMessage, Color color)
		{
			StopCoroutine(DeleteMessage());
			
			this.message.color = color;
			this.message.text  = inputMessage;
			
			StartCoroutine(DeleteMessage());
		}

		public IEnumerator DeleteMessage()
		{
			yield return new WaitForSeconds(5);

			message.text = string.Empty;
		}
	}
}