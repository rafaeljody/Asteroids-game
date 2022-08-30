using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public Sprite[] sprites; // array of sprites, tempat buat naruh spritesnya

    // sizing of asteroids
    // so, the big asteroid will become smaller if we hit it.
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifeTime = 30.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // change the sprites, based on array
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // randomize its rotation
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        // randomize its scale
        this.transform.localScale = Vector3.one * this.size; // this is the simple way to say [ new Vector3(this.size, this.size, this.size ]

        _rigidbody.mass = this.size; // the larger the size, the larger the mass

    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            // bad way, performance issues
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(gameObject);

        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid halfAsteroid = Instantiate(this, position, this.transform.rotation);
        halfAsteroid.size = this.size * 0.5f;
        halfAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }


}
