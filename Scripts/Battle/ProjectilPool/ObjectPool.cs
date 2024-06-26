using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    private void Awake()
    {
        base.Awake();
        if (transform.parent == null && gameObject.scene.name != null)
        {
            Transform parentTransform = transform.parent;
            while (parentTransform != null)
            {
                if (parentTransform.CompareTag("DontDestroyOnLoad"))
                {
                    Destroy(parentTransform.gameObject);
                    break;
                }
                parentTransform = parentTransform.parent;
            }
        }

        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject obj = PoolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        PoolDictionary[tag].Enqueue(obj);
        return obj;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Destroy(obj);
            return;
        }
        obj.SetActive(false);
        PoolDictionary[tag].Enqueue(obj);
    }
}
