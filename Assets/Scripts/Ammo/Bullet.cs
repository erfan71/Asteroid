﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ammo {
    public override void Fire(Vector2 force)
    {
        base.Fire(force);
        _rigidBody.AddForce(force);
    }
}
