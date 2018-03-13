using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity), typeof(NetworkTransform))]
public class ProjectileController : MonoBehaviour
{

    private static string TAG_PLAYER = "Player";

    private Rigidbody2D _rb;

    private float _speed;
    private Vector2 _direction;
    private bool _isMovingRight;
    private int _damage;

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
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TAG_PLAYER))
        {
            var controller = col.gameObject.GetComponent<PlayerController>();
            controller.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}