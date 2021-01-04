using System;
using System.Collections;
using Events.MoneyManagement;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Bounce
{
	public abstract class AbstractBounce<T> : BetterMonoBehaviour
		where T : VDEvent
	{
		[SerializeField]
		private float speed;

		private Vector3 targetPosition;
		private Vector3 oldPosition;

		private float time;
		protected bool isRunning;

		private void Start()
		{
			EventManager.Instance.AddListener<T>(StartBounce);
		}

		protected void StartBounce()
		{
			if (isRunning)
			{
				StopAllCoroutines();
				transform.position = oldPosition;
			}
			
	

			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(Bounce());
			}
		}

		protected IEnumerator Bounce()
		{
			print("Danny");
			isRunning      = true;
			oldPosition    = transform.position;
			targetPosition = transform.position + Vector3.up * 5;
			
			while (time < 1)
			{
				time               += Time.deltaTime / speed;
				transform.position =  Vector3.Lerp(oldPosition, targetPosition, time);
				yield return null;
			}

			//yield return new WaitForSeconds(0.1f);

			while (time > 0)
			{
				time               -= Time.deltaTime / speed;
				transform.position =  Vector3.Lerp(oldPosition, targetPosition, time);
				yield return null;
			}

			isRunning = false;
		}
	}
}