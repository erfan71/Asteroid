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

    private const int extentMargin = 5;

    private void Start()
    {
        StartCoroutine(SpawningRoutine());
    }

    public float GetRandomeSpawnSpeed()
    {
        float speed = Random.Range(asteroidSpawnSpeed_MinMax.x, asteroidSpawnSpeed_MinMax.y + 1);
        return speed;
    }
    public float GetRandomSpawnScale()
    {
        float scale = Random.Range(asteroidSpawnScale_MinMax.x, asteroidSpawnScale_MinMax.y + 1);
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

    public Quaternion CalculateDirectionToCenter(Vector3 fromPosition)
    {
        Vector3 diff = Vector3.zero - fromPosition;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    private void SpawnAsteroid()
    {
        Vector2 startPos = FindRandomeSpawnPosition();
        Quaternion rotation = CalculateDirectionToCenter(startPos);
        float speed = GetRandomeSpawnSpeed();
        float scale = GetRandomSpawnScale();
        Asteroid asteroid = ObjectPoolManager.Instance.GetObject<Asteroid>(asteroidObjectPoolKey);
        asteroid.Setup(asteroidsParent, startPos, rotation, speed, scale);
    }
    IEnumerator SpawningRoutine()
    {
        float i = 0.0f;
        
        while (i <= 1)
        {
            i += gettingHardRate * Time.deltaTime;
            float delay = Mathf.Lerp(spawnTime_MaxMin.x, spawnTime_MaxMin.y, i);
            Debug.Log(delay);
            SpawnAsteroid();
            yield return new WaitForSeconds(delay);
        }

    }

}
