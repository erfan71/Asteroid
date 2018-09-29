using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
        #region Singeleton
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ObjectPoolManager
>();
            }
            return instance;
        }
    }
    #endregion Singeleton
    // Use this for initialization
    [System.Serializable]
    public struct PoolableObjectConfig
    {
        public string key;
        public GameObject prefab;
        public int count;
        public PoolableObjectConfig(string key, GameObject prefab, int count)
        {
            this.key = key;
            this.prefab = prefab;
            this.count = count;
        }
    }
    public int defaultRegenerateCount = 100;
    public List<PoolableObjectConfig> objectsDetail;
    private Dictionary<string, List<GameObject>> objectsPool;

    public void Awake()
    {
        objectsPool = new Dictionary<string, List<GameObject>>();
        foreach (PoolableObjectConfig obj in objectsDetail)
        {
           
            objectsPool.Add(obj.key, InstantiateObjectForPool(obj));
        }

    }
    private List<GameObject> InstantiateObjectForPool(PoolableObjectConfig obj)
    {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < obj.count; i++)
        {
            GameObject objInstance = Instantiate(obj.prefab);
            objInstance.SetActive(false);
            objInstance.transform.SetParent(this.transform);
            objInstance.name = obj.prefab.name;
            objInstance.AddComponent<PoolableObjectInstance>().Key = obj.key;

            objects.Add(objInstance);
        }
        return objects;
    }
    public T GetObject<T>(string key) where T : class
    {
        if (objectsPool.ContainsKey(key))
        {
            List<GameObject> objectWithThisKey = objectsPool[key];
            if (objectWithThisKey.Count > 1)
            {
                GameObject temp = objectWithThisKey[1];
                temp.SetActive(true);
                temp.transform.SetParent(null);
                objectWithThisKey.RemoveAt(1);
                return temp.GetComponent<T>();
            }
            else
            {
                PoolableObjectConfig objconf = new PoolableObjectConfig(key, objectWithThisKey[0], defaultRegenerateCount);
                objectWithThisKey.AddRange(InstantiateObjectForPool(objconf));

                GameObject temp = objectWithThisKey[1];
                temp.SetActive(true);
                temp.transform.SetParent(null);
                objectWithThisKey.RemoveAt(1);
                return temp.GetComponent<T>();
            }       
        }
        else
        {
            Debug.LogError("No Object With This Key");
            return null;
        }

    }
    public void RecyleObject(PoolableObjectInstance poi)
    {
        poi.gameObject.SetActive(false);
        poi.transform.SetParent(this.transform);
        objectsPool[poi.Key].Add(poi.gameObject);
    }
}

