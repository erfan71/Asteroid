using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Rigidbody2D _rigidBody;
    public System.Action<Asteroid, Collider2D> AsteroidCollisionAction;
    private float setupTime;
    private float speed;

    private const float sheildTime = 0.5f;

    public void Setup(Transform parent, Vector2 startPos, Quaternion startRotation, float speed, float scale)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        transform.position = startPos;
        transform.rotation = startRotation;
        transform.parent = parent;
        transform.localScale *= scale;
        Vector3 velocity = transform.rotation * Vector3.up;
        this.speed = speed;
        _rigidBody.AddRelativeForce(velocity * speed);
        setupTime = Time.time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - setupTime > sheildTime)
            if (AsteroidCollisionAction != null)
                AsteroidCollisionAction(this, collision);
    }
    public float GetVelocity()
    {
        return speed;
    }
}
