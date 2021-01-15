using System.Collections.Generic;
using System.Linq;
using Interfaces;
using VDFramework;

namespace Utility.ListenerAdders
{
	public class ListenerAdder : BetterMonoBehaviour, IListener
	{
		private IEnumerable<IListener> listeners;
		
		public void AddListeners()
		{
#pragma warning disable 252,253 // Reference comparison is intended
			listeners = GetComponents<IListener>().Where(listener => listener != this);
#pragma warning restore 252,253
			
			foreach (IListener listener in listeners)
			{
				listener.AddListeners();
			}
		}

		public void RemoveListeners()
		{
			foreach (IListener listener in listeners)
			{
				listener.RemoveListeners();
			}
		}
	}
}