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
                instance = GameObject.FindObjectOfType<ObjectPoolManager>();
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
            objInstance.AddComponent<PoolableObjectInstance>();
            PoolableObjectInstance poolableRef = objInstance.GetComponent<PoolableObjectInstance>();
            poolableRef.Key = obj.key;
            poolableRef.UseStatus = PoolableObjectInstance.UsageStatus.Ready;
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
                return PrepareItemtoExit(objectWithThisKey).GetComponent<T>();
            }
            else
            {
                PoolableObjectConfig objconf = new PoolableObjectConfig(key, objectWithThisKey[0], defaultRegenerateCount);
                objectWithThisKey.AddRange(InstantiateObjectForPool(objconf));
              
                return PrepareItemtoExit(objectWithThisKey).GetComponent<T>();
            }       
        }
        else
        {
            Debug.LogError("No Object With This Key");
            return null;
        }

    }
    GameObject PrepareItemtoExit(List<GameObject> selectedList)
    {
        GameObject temp = selectedList[1];
        temp.SetActive(true);
        temp.transform.SetParent(null);
        selectedList.RemoveAt(1);
        temp.GetComponent<PoolableObjectInstance>().UseStatus = PoolableObjectInstance.UsageStatus.InUse;
        return temp;
    }
    public void RecycleObject(PoolableObjectInstance poi)
    {
        if (poi.UseStatus == PoolableObjectInstance.UsageStatus.InUse)
        {
            poi.gameObject.SetActive(false);
            poi.transform.SetParent(this.transform);
            poi.UseStatus = PoolableObjectInstance.UsageStatus.Ready;
            objectsPool[poi.Key].Add(poi.gameObject);
        }
       
    }
}

