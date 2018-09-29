using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon {

    public Transform MainBarrel;

    public override void Fire(Vector2 direction)
    {
        if (Time.time - lastFireTime > fireRate)
        {
            Ammo ammoInstance = ObjectPoolManager.Instance.GetObject<Ammo>(ammoKey);
            ammoInstance.transform.position = MainBarrel.transform.position;
            ammoInstance.transform.parent = ammoParents;
            ammoInstance.Setup();
            ammoInstance.Fire(new Vector2(direction.x * firePower, direction.y * firePower));
            lastFireTime = Time.time;
        }

    }

}
