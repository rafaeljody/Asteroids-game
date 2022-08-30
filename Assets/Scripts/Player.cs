using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 0.1f;

    private bool _thrusting;
    private float _turnDirection;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // thrusting
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);


        // rotate
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        // shoot
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }


    }

    private void FixedUpdate()
    {
        // moving our player
        if (_thrusting)
        {
            _rigidBody.AddForce(this.transform.up * thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidBody.AddTorque(_turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        // instantiate the bullet
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        // project the bullet in the face of thursting
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularDrag = 0.0f;

            // turn of our gameObject entirely
            this.gameObject.SetActive(false);

            // bad way, performance issues
            FindObjectOfType<GameManager>().PlayerDied();

        }
    }
}
