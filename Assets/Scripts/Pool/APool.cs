using System.Collections.Generic;
using UnityEngine;

namespace Kekw.Pool
{
    /// <summary>
    /// Base class for different types of pools.
    /// </summary>
    public abstract class APool : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Pool object prefab, must implement IPoolMember interface.")]
        GameObject _prefab;

        [SerializeField]
        [Tooltip("Pool size")]
        int _size;

        private Queue<APoolMember> _pool;

        private void Awake()
        {
            _pool = new Queue<APoolMember>();
            // initialize pool
            for (int i = 0; i < _size; i++)
            {
                GameObject temp = Instantiate(_prefab);
                temp.SetActive(false);
                temp.transform.position = this.transform.position;
                _pool.Enqueue(temp.GetComponent<APoolMember>());
            }
        }

        /// <summary>
        /// Get IPoolMember from pool.
        /// </summary>
        /// <returns></returns>
        public APoolMember GetFromPool()
        {
            APoolMember poolMember = _pool.Dequeue();
            poolMember.gameObject.SetActive(true);
            poolMember.SetOwnerPool(this);
            return poolMember;
        }

        /// <summary>
        /// Return to poolmember to pool.<br></br><br></br>
        /// Sets returning object inactive and moves it to pool origin.
        /// </summary>
        /// <param name="poolMember"></param>
        public void ReturnToPool(GameObject poolMember)
        {
            poolMember.SetActive(false);
            poolMember.transform.position = this.transform.position;
            _pool.Enqueue(poolMember.GetComponent<APoolMember>());
        }
    } 
}
