using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ammo : MonoBehaviour
{
    protected Rigidbody2D _rigidBody;
    public void Setup()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    public virtual void Fire(Vector2 force)
    {
    }

}
