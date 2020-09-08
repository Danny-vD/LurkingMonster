using UnityEngine;
using VDFramework;

namespace Temporary
{
	public class WallSpawner : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject wallPrefab = null;

		[SerializeField]
		private float modelHeight = 10;

		[SerializeField]
		private float modelWidth = 10;

		[SerializeField]
		private float wallHeight = 5;

		private void Awake()
		{
			Vector3 scale = CachedTransform.localScale;

			Spawnwall(scale.z, CachedTransform.forward, 0.0f);    // Back wall
			Spawnwall(scale.x, CachedTransform.right, 90.0f);     // Right wall
			Spawnwall(scale.z, -CachedTransform.forward, 180.0f); // Front wall
			Spawnwall(scale.x, -CachedTransform.right, 270.0f);   // left wall
		}

		private void Spawnwall(float scale, Vector3 direction, float rotation = 0.0f)
		{
			GameObject wall = Instantiate(wallPrefab, CachedTransform);
			wall.transform.position = CachedTransform.position + scale * 5 * direction;
			wall.transform.rotation = Quaternion.Euler(-90, 0, rotation);

			// Make sure that the wall covers the whole width of the ground by scaling X
			wall.transform.localScale = new Vector3(10 / modelWidth, 1, 1);

			SetWallHeight(wall);
		}

		// Move the wall up or down if the desired height is within our limit, or scale if outside our limit
		private void SetWallHeight(GameObject wall)
		{
			float currentHeight = modelHeight / 2.0f;

			if (Mathf.Approximately(currentHeight, wallHeight))
			{
				return;
			}

			if (wallHeight <= 0)
			{
				wall.transform.Translate(0, 0, -currentHeight);
				return;
			}

			float deltaHeight = wallHeight - currentHeight;

			if (deltaHeight <= currentHeight)
			{
				wall.transform.Translate(0, 0, deltaHeight);
				return;
			}

			wall.transform.localScale = new Vector3(wall.transform.localScale.x, 1, wallHeight / currentHeight);
		}
	}
}