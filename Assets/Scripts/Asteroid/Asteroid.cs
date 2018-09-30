using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    private Rigidbody2D _rigidBody;
	public void Setup(Transform parent, Vector2 startPos, Quaternion startRotation, float speed,float scale)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        transform.position = startPos;
        transform.rotation = startRotation;
        transform.parent = parent;
        transform.localScale *= scale;
        Vector3 velocity = transform.rotation * Vector3.up;

        _rigidBody.AddRelativeForce(velocity * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
