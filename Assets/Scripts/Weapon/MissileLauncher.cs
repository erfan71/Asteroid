using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon {

    public Transform MainBarrel;

    public override void Fire(Vector2 direction)
    {
        if (Time.time - lastFireTime > fireRate)
        {
            Ammo ammoInstance = GetObjectFromPool();
            ammoInstance.transform.position = MainBarrel.transform.position;
            ammoInstance.transform.parent = ammoParents;
            ammoInstance.Setup();
            ammoInstance.Fire(new Vector2(direction.x * firePower, direction.y * firePower));
            lastFireTime = Time.time;

            base.Fire(direction);

            if (WeaponfireAction != null)
                WeaponfireAction(this);
            FireParticle();

        }

    }
    private void FireParticle()
    {
        Transform obj = ObjectPoolManager.Instance.GetObject<Transform>(fireParticlePoolName);
        obj.position = MainBarrel.transform.position;
    }
}
