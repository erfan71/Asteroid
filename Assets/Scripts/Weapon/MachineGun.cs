using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon {

    public Transform MainBarrel;
    public Transform RightBarrel;
    public Transform LeftBarrel;


    public override void Fire(Vector2 direction)
    {
        if (Time.time - lastFireTime > fireRate)
        {
            Ammo ammoInstance1 = GetObjectFromPool();  
            Ammo ammoInstance2 = GetObjectFromPool();
            Ammo ammoInstance3 = GetObjectFromPool();

            ammoInstance1.transform.position = MainBarrel.transform.position;
            ammoInstance1.transform.parent = ammoParents;
            ammoInstance1.Setup();
            ammoInstance1.Fire(new Vector2(direction.x * firePower, direction.y * firePower));

            ammoInstance2.transform.position = LeftBarrel.transform.position;
            ammoInstance2.transform.parent = ammoParents;
            ammoInstance2.Setup();
            ammoInstance2.Fire(new Vector2(direction.x * firePower, direction.y * firePower));

            ammoInstance3.transform.position = RightBarrel.transform.position;
            ammoInstance3.transform.parent = ammoParents;
            ammoInstance3.Setup();
            ammoInstance3.Fire(new Vector2(direction.x * firePower, direction.y * firePower));
            lastFireTime = Time.time;
        }

    }

}
