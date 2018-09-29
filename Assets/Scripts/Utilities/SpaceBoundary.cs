using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBoundary : MonoBehaviour
{

    public struct SpaceExtent
    {
        public float Min_x;
        public float Max_x;
        public float Min_y;
        public float Max_y;

        public SpaceExtent(float Min_x, float Max_x, float Min_y, float Max_y)
        {
            this.Min_x = Min_x;
            this.Max_x = Max_x;
            this.Min_y = Min_y;
            this.Max_y = Max_y;
        }
        public void Log()
        {
            Debug.LogFormat("Min_x: {0}, Max_x: {1}, Min_y: {2}, Max_y: {3}", Min_x, Max_x, Min_y, Max_y);
        }
    }

    private SpaceExtent screenRect_WorldPosition;
    public SpaceExtent ScreenRect_WorldPosition
    {
        get { return screenRect_WorldPosition; }
        set { screenRect_WorldPosition = value; }
    }
    private void Awake()
    {
        CalcualteBoundry();
    }
    private void CalcualteBoundry()
    {
        Camera main = Camera.main;
        float aspect = main.aspect;
        float orthoSize = main.orthographicSize;
        Vector3 cameraPosition = main.transform.position;

        float horizontalExtent = aspect * orthoSize;


        float rightX = horizontalExtent + cameraPosition.x;
        float leftX = cameraPosition.x - horizontalExtent;
        float upY = orthoSize + cameraPosition.y;
        float downY = cameraPosition.y - orthoSize;

        ScreenRect_WorldPosition = new SpaceExtent(leftX, rightX, downY, upY);
    }

}
