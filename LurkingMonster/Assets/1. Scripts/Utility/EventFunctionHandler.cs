using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
	public class EventFunctionHandler : AbstractFunctionHandler
	{
		[SerializeField]
		private UnityEvent unityAction;
		
		protected override void ReactToEvent(UnityFunction unityFunction)
		{
			unityAction.Invoke();
		}
	}
}