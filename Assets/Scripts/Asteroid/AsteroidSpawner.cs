using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public Vector2 asteroidSpawnSpeed_MinMax;
    public Vector2 asteroidSpawnScale_MinMax;
    public SpaceBoundary boundry;
    public string asteroidObjectPoolKey;
    public Transform asteroidsParent;

    public Vector2 spawnTime_MaxMin;
    public float gettingHardRate;

    private const float BulletScaleReduction = 0.55f;
    private const float MissileScaleReduction = 0.25f;
    private const string BulletTag = "Bullet";
    private const string MissileTag = "Missile";


    private const int extentMargin = 5;

    public System.Action<Asteroid> AsteroidDestroyedAction;

    public void StartSpawning()
    {
        StartCoroutine(SpawningRoutine());
    }

    public float GetRandomeSpawnSpeed()
    {
        float speed = Random.Range(asteroidSpawnSpeed_MinMax.x, asteroidSpawnSpeed_MinMax.y);
        return speed;
    }
    public float GetRandomSpawnScale()
    {
        float scale = Random.Range(asteroidSpawnScale_MinMax.x, asteroidSpawnScale_MinMax.y);
        return scale;
    }
    public Vector2 FindRandomeSpawnPosition()
    {

        int selectedSide = Random.Range(0, 4);
        float calculatedX = 0;
        float calculatedY = 0;

        SpaceBoundary.SpaceExtent extent = boundry.ScreenRect_WorldPosition;
        switch (selectedSide)
        {
            //right
            case 0:
                calculatedX = extent.Max_x;
                calculatedY = Random.Range(extent.Min_y - extentMargin, extent.Max_y + extentMargin);
                break;
            //up
            case 1:
                calculatedX = Random.Range(extent.Min_x - extentMargin, extent.Max_x + extentMargin);
                calculatedY = extent.Max_y;
                break;

            //left
            case 2:
                calculatedX = extent.Min_x;
                calculatedY = Random.Range(extent.Min_y - extentMargin, extent.Max_y + extentMargin);
                break;
            //down
            case 3:
                calculatedX = Random.Range(extent.Min_x - extentMargin, extent.Max_x + extentMargin);
                calculatedY = extent.Min_y;
                break;

            default:
                calculatedX = extent.Max_x;
                calculatedY = Random.Range(extent.Min_y - extentMargin, extent.Max_y + extentMargin);
                break;
        }
        return new Vector2(calculatedX, calculatedY);
    }

    public Vector2 CalculateDirectionToCenter(Vector3 fromPosition)
    {
        Vector3 diff = Vector3.zero - fromPosition;
        return diff.normalized;
    }
    private void SpawnAsteroid()
    {
        Vector2 startPos = FindRandomeSpawnPosition();
        Vector2 direction = CalculateDirectionToCenter(startPos);
        float speed = GetRandomeSpawnSpeed();
        float scale = GetRandomSpawnScale();
        Asteroid asteroid = ObjectPoolManager.Instance.GetObject<Asteroid>(asteroidObjectPoolKey);
        asteroid.Setup(asteroidsParent, startPos, direction, speed, scale);
        asteroid.AsteroidCollisionAction += OnAsteroidCollisionCallack;
    }
    private void SpawnAsteroid(Vector2 startPos, Vector2 direction, float speed, float scale)
    {
        Asteroid asteroid = ObjectPoolManager.Instance.GetObject<Asteroid>(asteroidObjectPoolKey);
        asteroid.Setup(asteroidsParent, startPos, direction, speed, scale);
        asteroid.AsteroidCollisionAction += OnAsteroidCollisionCallack;
    }
    private void OnAsteroidCollisionCallack(Asteroid asteroid, Collider2D obj)
    {
        if (obj.tag == BulletTag)
        {
            if (asteroid.transform.localScale.x * BulletScaleReduction >= 0.25f)
            {
                Vector2 asteroidDirection = asteroid.GetDirection();
                Vector2 dir1 = Quaternion.Euler(0, 0, 30) * asteroidDirection;
                dir1 = dir1.normalized;
                Vector2 dir2 = Quaternion.Euler(0, 0, -30) * asteroidDirection;
                dir2 = dir2.normalized;
                SpawnAsteroid(asteroid.transform.position, dir1, asteroid.GetSpeed(), asteroid.transform.localScale.x * BulletScaleReduction);
                SpawnAsteroid(asteroid.transform.position, dir2, asteroid.GetSpeed(), asteroid.transform.localScale.x * BulletScaleReduction);
            }
            asteroid.Recycle();
            obj.GetComponent<Ammo>().Recycle();
            if(AsteroidDestroyedAction!=null)
                AsteroidDestroyedAction(asteroid);
        }
        else if (obj.tag == MissileTag)
        {

            if (asteroid.transform.localScale.x * MissileScaleReduction >= 0.25f)
            {
                Vector2 asteroidDirection = asteroid.GetDirection();

                Vector2 dir1 = Quaternion.Euler(0, 0, 15) * asteroidDirection;
                dir1 = dir1.normalized;
                Vector2 dir2 = Quaternion.Euler(0, 0, 45) * asteroidDirection;
                dir2 = dir2.normalized;

                Vector2 dir3 = Quaternion.Euler(0, 0, -15) * asteroidDirection;
                dir3 = dir3.normalized;
                Vector2 dir4 = Quaternion.Euler(0, 0, -45) * asteroidDirection;
                dir4 = dir4.normalized;

                SpawnAsteroid(asteroid.transform.position, dir1, asteroid.GetSpeed(), asteroid.transform.localScale.x * MissileScaleReduction);
                SpawnAsteroid(asteroid.transform.position, dir2, asteroid.GetSpeed(), asteroid.transform.localScale.x * MissileScaleReduction);
                SpawnAsteroid(asteroid.transform.position, dir3, asteroid.GetSpeed(), asteroid.transform.localScale.x * MissileScaleReduction);
                SpawnAsteroid(asteroid.transform.position, dir4, asteroid.GetSpeed(), asteroid.transform.localScale.x * MissileScaleReduction);
                
            }
            asteroid.Recycle();
            obj.GetComponent<Ammo>().Recycle();
            if (AsteroidDestroyedAction != null)
                AsteroidDestroyedAction(asteroid);
        }
    }

    IEnumerator SpawningRoutine()
    {
        float i = 0.0f;

        while (true)
        {
            i += gettingHardRate * Time.deltaTime;
            i = Mathf.Clamp(i, 0, 1);
            float delay = Mathf.Lerp(spawnTime_MaxMin.x, spawnTime_MaxMin.y, i);
            SpawnAsteroid();
            yield return new WaitForSeconds(delay);
        }

    }

}
