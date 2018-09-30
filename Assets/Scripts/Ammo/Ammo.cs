using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ammo : MonoBehaviour
{
    protected Rigidbody2D _rigidBody;
    public int timeToDestroy = 10;
    public void Setup()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
      
    }
    public virtual void Fire(Vector2 force)
    {
        Invoke("Recycle", timeToDestroy);
    }
    
    public void Recycle()
    {
        _rigidBody.velocity = Vector2Int.zero;
        _rigidBody.angularVelocity = 0;
        CancelInvoke("Recycle");
        ObjectPoolManager.Instance.RecycleObject(GetComponent<PoolableObjectInstance>());
    }

}
