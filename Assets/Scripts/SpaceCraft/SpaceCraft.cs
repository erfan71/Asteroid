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
    public SpaceCraftHUDManager spaceHUd;

    public ParticleSystem accelerateParticle;

    private Vector2 input;
    private Rigidbody2D rigidBody;

    private const string AsteroidTag = "Asteroid";

    public float MaxHealth;
    private float health;
    private const float damageMaginifire = 1f;

    public System.Action ZeroHealthAction;
    public System.Action MissileFireAction;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        health = MaxHealth;
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

       
        HandleAccelerateParticle(input.y);
    }
    void HandleAccelerateParticle(float yInput)
    {
        if (input.y == 0)
        {
            if (!accelerateParticle.isStopped)
            {
                accelerateParticle.Stop();
            }
        }
        else
        {
            if (!accelerateParticle.isPlaying)
            {
                accelerateParticle.Play();
            }
        }
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
        {
            machinGun.Fire(transform.up);
           
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            launcher.Fire(transform.up);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == AsteroidTag)
        {
            HandleAsteroidCollision(collision.GetComponent<Asteroid>());
        }
    }
    void HandleAsteroidCollision(Asteroid asteroid)
    {
        float damage = DamageCalculator(asteroid.transform.localScale.x, asteroid.GetRigidBodySpeed());

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            ZeroHealthAction?.Invoke();
        }
        else
        {
            spaceHUd.SetHealthBarVar(health / MaxHealth);
        }
    }
    float DamageCalculator(float asteroidScale, float asteroidSpeed)
    {
        return asteroidScale * asteroidSpeed * damageMaginifire * GetSpeed();
    }
    float GetSpeed()
    {
        return Mathf.Clamp(rigidBody.velocity.magnitude, 1, 100);
    }
}
