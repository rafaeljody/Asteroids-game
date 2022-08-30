using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxLifeTime = 10.0f;
    public float speed = 500.0f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        // add force, so the bullet will shooting
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

}
