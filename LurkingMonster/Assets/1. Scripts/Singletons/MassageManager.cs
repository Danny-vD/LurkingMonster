using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.Singleton;

namespace Singletons
{
	public class MassageManager : Singleton<MassageManager>
	{
		private Text message;

		protected override void Awake()
		{
			base.Awake();
			message = GetComponent<Text>();
		}

		public void ShowMessage(string inputMessage, Color color)
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