using System;
using Temporary.Singletons;
using UnityEngine;
using VDFramework;

namespace Temporary
{
    public class CollectableSpawner : BetterMonoBehaviour
    {
        [SerializeField]
        private GameObject collectable = null;

        private void Awake()
        {
            CollectableManager.Instance.RegisterSpawner(this);
        }

        public void Spawn()
        {
            Instantiate(collectable, CachedTransform.position, CachedTransform.rotation);
            
            Destroy(gameObject);
        }
    }
}
