using UnityEngine;

namespace Interfaces
{
	public interface ISelectable
	{
		void Select(Material selectMaterial);

		void Deselect();
	}
}