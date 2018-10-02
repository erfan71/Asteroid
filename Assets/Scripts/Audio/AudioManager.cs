using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip impactClip;
    public AudioClip bulletClip;
    public AudioClip missileClip;

    public AudioSource source;

    public AsteroidSpawner asteroidSpawner;

    private void Start()
    {
        Weapon.WeaponfireAction += WeaponfireCallBack;
        asteroidSpawner.AsteroidDestroyedAction += AsteroidDestroyedCallBack;

    }
    private void OnDestroy()
    {
        Weapon.WeaponfireAction -= WeaponfireCallBack;

        asteroidSpawner.AsteroidDestroyedAction -= AsteroidDestroyedCallBack;
    }
    private void WeaponfireCallBack(Weapon weapon)
    {
        if (weapon is MissileLauncher)
            source.PlayOneShot(missileClip);
        else if (weapon is MachineGun)
            source.PlayOneShot(bulletClip);

    }


    private void AsteroidDestroyedCallBack(Asteroid obj)
    {
        source.PlayOneShot(impactClip);
    }


}
