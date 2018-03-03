using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private readonly string INPUT_HORIZONTAL = "Horizontal2";
    private readonly string INPUT_VERTICAL = "Vertical2";
    private readonly string SPEED_ATTRIBUTE = "Speed";
    private readonly string ANIM_DIE = "Dead";
    private readonly string ANIM_ATTACK = "Attack";

    public float Speed = 2f;
    public int Health = 100;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    private Vector2 _movementDirection;
    private bool _canMove;
   
    // Use this for initialization
    void Start ()
	{
	    _rb = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();
	    _sr = GetComponent<SpriteRenderer>();

	    _canMove = true;
	}

    void FixedUpdate ()
    {
        _rb.velocity = _movementDirection * Speed;
    }

    void Update()
    {
        if (_canMove)
            _movementDirection = new Vector2(Input.GetAxisRaw(INPUT_HORIZONTAL), Input.GetAxisRaw(INPUT_VERTICAL))
                .normalized;
        else
            _movementDirection = Vector2.zero;

        Speed = Input.GetKey(KeyCode.LeftShift) ? 5 : 2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(ANIM_ATTACK);
            _movementDirection = Vector2.zero;
            _canMove = false;
        }

        //This is only for testing "Die" animation. TODO: Remove.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.SetTrigger(ANIM_DIE);
            _canMove = false;
        }

        ChangeDirection();
        ChangeAnimation();
    }

    private void ChangeDirection()
    {
        if (_movementDirection.x < 0)
        {
            _sr.flipX = true;
        } else if (_movementDirection.x > 0)
        {
            _sr.flipX = false;
        }
    }

    private void ChangeAnimation()
    {
        _animator.SetFloat(SPEED_ATTRIBUTE, _rb.velocity.magnitude);
        
    }

    private void Die()
    {
        _animator.SetTrigger(ANIM_DIE);
        _canMove = false;
    }

    public void OnAttackEnd()
    {
        _canMove = true;
    }
}
