using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public string ammoKey;
    public Transform ammoParents;
    public float firePower;
    public float fireRate;

    protected float lastFireTime = -Mathf.Infinity;
    public virtual void Fire(Vector2 direction)
    {

    }
    protected Ammo GetObjectFromPool()
    {
       return ObjectPoolManager.Instance.GetObject<Ammo>(ammoKey);
    }

}
