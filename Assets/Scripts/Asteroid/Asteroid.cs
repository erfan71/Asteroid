using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Rigidbody2D _rigidBody;
    public System.Action<Asteroid, Collider2D> AsteroidCollisionAction;
    private float setupTime;
    private float speed;

    private const float sheildTime = 0.25f;

    public void Setup(Transform parent, Vector2 startPos, Vector2 direction, float speed, float scale)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        transform.position = startPos;
        transform.localScale = new Vector3(scale, scale, scale);
        transform.parent = parent;      
        this.speed = speed;
          _rigidBody.AddRelativeForce(direction * speed);
        _rigidBody.AddTorque(2*speed);
        setupTime = Time.time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - setupTime > sheildTime)
            if (AsteroidCollisionAction != null)
                AsteroidCollisionAction(this, collision);
    }
    public float GetSpeed()
    {
        return speed;
    }
    public Vector2 GetDirection()
    {
        return _rigidBody.velocity.normalized;
    }
    public void Recycle()
    {
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0;
        AsteroidCollisionAction = null;
        transform.rotation = Quaternion.identity;
        ObjectPoolManager.Instance.RecycleObject(gameObject.GetComponent<PoolableObjectInstance>());

    }
}
