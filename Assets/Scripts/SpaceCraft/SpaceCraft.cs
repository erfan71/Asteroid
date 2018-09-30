using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraft : MonoBehaviour
{
    public float turningSpeed;
    public float movementSpeed;
    public float maximumSpeed;
    public SpaceBoundary spaceBoundry;
    public MachineGun machinGun;
    public MissileLauncher launcher;

    private Vector2 input;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MovementHandling();
        SpaceCroftBoundryCheck();
        FireHandling();
    }


    #region Movement
    void MovementHandling()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //eliminating the backward movement
        vertical = Mathf.Clamp(vertical, 0, 1);
        input = new Vector2(horizontal, vertical);

        transform.Rotate(0, 0, -input.x * turningSpeed * Time.deltaTime);
        rigidBody.AddRelativeForce(new Vector2(0, input.y * movementSpeed));
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maximumSpeed);

    }
    void SpaceCroftBoundryCheck()
    {
        SpaceBoundary.SpaceExtent screenExtent = spaceBoundry.ScreenRect_WorldPosition;
        if (transform.position.x > screenExtent.Max_x)
        {
            SpaceCraftExit_Right();
        }
        else if (transform.position.x < screenExtent.Min_x)
        {
            SpaceCraftExit_Left();
        }
        else if (transform.position.y > screenExtent.Max_y)
        {
            SpaceCraftExit_Up();
        }
        else if (transform.position.y < screenExtent.Min_y)
        {
            SpaceCraftExit_Down();
        }
    }
    void SpaceCraftExit_Right()
    {
        transform.position = new Vector3(spaceBoundry.ScreenRect_WorldPosition.Min_x, transform.position.y);
    }
    void SpaceCraftExit_Left()
    {
        transform.position = new Vector3(spaceBoundry.ScreenRect_WorldPosition.Max_x, transform.position.y);
    }
    void SpaceCraftExit_Up()
    {
        transform.position = new Vector3(transform.position.x, spaceBoundry.ScreenRect_WorldPosition.Min_y);
    }
    void SpaceCraftExit_Down()
    {
        transform.position = new Vector3(transform.position.x, spaceBoundry.ScreenRect_WorldPosition.Max_y);
    }
    #endregion

    void FireHandling()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            machinGun.Fire(transform.up);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            launcher.Fire(transform.up);
        }
    }
}
