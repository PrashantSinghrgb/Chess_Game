using System.Collections.Generic;
using UnityEngine;

namespace PrashantSingh.Game
{
    /// An abstract class which spawners of a single object can extend from.
    public abstract class SingleObjectPooler : MonoBehaviour
    {
		/// <summary>The object to pool.</summary>
		[Tooltip("The object to pool.")]
		[SerializeField] protected GameObject objectToPool;
		/// <summary>A list of pooled objects.</summary>
		private List<GameObject> _pooledObjects;
		/// <summary>The initial pooled amount.</summary>
		[Tooltip("The initial pooled amount.")]
		[SerializeField] protected int initialPoolAmount = 5;
		/// <summary>Whether the list of pooled objects can grow.</summary>
		[Tooltip("Whether the list of pooled objects can grow.")]
		[SerializeField] protected bool canGrow = false;
		/// <summary>The maximum number of objects that can be pooled.</summary>
		[Tooltip("The initial pooled amount.")]
		[SerializeField] protected int MAX_POOL_AMOUNT = 5;

		/// <summary>Callback when the instance is started.</summary>
		private void Start()
		{
			//create a list of pooled objects
			_pooledObjects = new List<GameObject>(initialPoolAmount);
			for (int i = 0; i < initialPoolAmount; i++)
			{
				GameObject obj = Instantiate(objectToPool) as GameObject;
				obj.SetActive(false);
				_pooledObjects.Add(obj);
			}
			//call a subclasses custom implementation
			StartImplementation();
		}

		/// <summary>An implementation which subclasses can override.</summary>
		protected virtual void StartImplementation() { }

		/// <summary>Returns an unused object from the pool. Can be null.</summary>
		public GameObject GetPooledObject()
		{
			//if there is an unactive object, return that
			for (int i = 0; i < _pooledObjects.Count; i++)
			{
				if (!_pooledObjects[i].activeInHierarchy) { return _pooledObjects[i]; }
			}
			//otherwise if the list can grown and is less than it's maximum size, create a new object and return that
			if (canGrow && _pooledObjects.Count < MAX_POOL_AMOUNT)
			{
				GameObject obj = Instantiate(objectToPool) as GameObject;
				_pooledObjects.Add(obj);
				return obj;
			}
			//otherwise there is no object available - return null
			return null;
		}
	}
}