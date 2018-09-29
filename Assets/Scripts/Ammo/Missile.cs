using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Ammo {

    public override void Fire(Vector2 force)
    {
        base.Fire(force);
        StartCoroutine(AddingForceRoutine(force));
    }
    IEnumerator AddingForceRoutine(Vector2 force)
    {
        float duration = 1;
        float rate = 1 / duration;
        float i = 0.0f;
        while(i <= 1 )
        {
            i += rate * Time.deltaTime;
            float degree = Mathf.Lerp(0, 90, i);
            float forceSin= Mathf.Sin(degree * Mathf.Deg2Rad);
            _rigidBody.AddForce(force * forceSin);
            yield return null;
        }
    }
}
