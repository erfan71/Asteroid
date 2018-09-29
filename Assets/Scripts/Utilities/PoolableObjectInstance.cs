using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObjectInstance : MonoBehaviour {

    private string key;
    public string Key
    {
        get
        {
            return key;
        }
        set
        {
            key = value;
        }
    }

    public enum UsageStatus
    {
        Ready,
        InUse,
    }

    private UsageStatus useStatus;
    public UsageStatus UseStatus
    {
        get
        {
           return useStatus;
        }
        set
        {
            useStatus = value;
        }
    }
}
