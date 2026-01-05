using System.Collections.Generic;
using UnityEngine;
using MyStudio.Core.Architecture; 

namespace MyStudio.Core.Optimization
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;           
            public GameObject prefab;  
            public int size;             
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        protected override void Awake()
        {
            base.Awake();
            InitializePools();
        }

        private void InitializePools()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

               
                GameObject poolParent = new GameObject("Pool_" + pool.tag);
                poolParent.transform.SetParent(this.transform);

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false); 
                    obj.transform.SetParent(poolParent.transform); 
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool dengan tag {tag} tidak ditemukan!");
                return null;
            }

        
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

 
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

          
            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

 
            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        
        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}