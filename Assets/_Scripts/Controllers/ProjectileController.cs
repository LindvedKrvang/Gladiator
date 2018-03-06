using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private Rigidbody2D _rb;

    private float _speed;
    private Vector2 _direction;
    private bool _isMovingRight;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = 10f;

    }

    public void SetDirection(bool isDirectionRight)
    {
        if (isDirectionRight)
        {
            _direction = new Vector2(1, 0);
        }
        else
        {
            _direction = new Vector2(-1, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            FlipParticles();
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Projectile collided");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }

    private void FlipParticles()
    {
        var particles = GetComponentsInChildren<ParticleLayerSorter>();
        if (particles == null) return;
        foreach (var particle in particles)
        {
            particle.FlipParticle();
        }
        var collider = GetComponent<CapsuleCollider2D>();
        var colPosition = collider.offset;
        colPosition.x += 2;
        collider.offset = colPosition;
    }
}