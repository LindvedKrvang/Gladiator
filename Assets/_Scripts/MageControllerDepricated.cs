using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string ANIM_WALK = "Walk";
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_ATTACK = "Attack";

    public float Speed = 1f;
    public string AttackButton = "return";
    public string PlayerNumber = "";
    public string CharacterClass = "";
    public GameObject AttackProjectile;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;

    private Vector2 _directionMovement;
    private bool _isFacingRight;
    private bool _isMoving;
    private bool _isCasting;

    private string _horizontal = "Horizontal";
    private string _vertical = "Vertical";

	// Use this for initialization
	void Start ()
	{
	    _isFacingRight = true;
	    _isMoving = false;
	    _isCasting = false;
	    _rb = GetComponent<Rigidbody2D>();
	    _sr = GetComponent<SpriteRenderer>();
	    _animator = GetComponent<Animator>();

	    _horizontal = _horizontal + PlayerNumber;
	    _vertical = _vertical + PlayerNumber;
	}

    void FixedUpdate()
    {
        if (!_isCasting)
        {
            _rb.velocity = _directionMovement * Speed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
	
	// Update is called once per frame
	void Update () {
		_directionMovement = new Vector2(Input.GetAxisRaw(_horizontal), Input.GetAxisRaw(_vertical)).normalized;
        CheckDirection();
	    CheckAnimations();
    }

    private void CheckDirection()
    {
        if (Input.GetAxisRaw(_horizontal) < 0 && _isFacingRight)
        {
            _sr.flipX = true;
            _isFacingRight = false;
        }
        else if (Input.GetAxisRaw(_horizontal) > 0 && !_isFacingRight)
        {
            _sr.flipX = false;
            _isFacingRight = true;
        }
    }
    private void CheckAnimations()
    {
        if (_directionMovement != Vector2.zero && !_isMoving)
        {
            if (!_isCasting)
            {
                _isMoving = true;
                _animator.Play(ANIM_WALK);
            }
        }
        else if (_directionMovement == Vector2.zero && _isMoving)
        {
            if (!_isCasting)
            {
                _isMoving = false;
                _animator.Play(ANIM_IDLE);
            }
        }

        if (Input.GetKeyDown(AttackButton))
        {
            _animator.Play(ANIM_ATTACK);
            _isCasting = true;
        }
        
    }

    void Attack()
    {
        if (AttackProjectile != null)
        {
            switch (CharacterClass)
            {
                case "Mage":
                {
                    MageAttack();
                    break;
                }
                default:
                {
                    break;
                }
            }
            
        }
        else
        {
            Debug.Log("AttackProjectile is null");
        }
    }

    private void MageAttack()
    {
        var position = transform.position;
        position.y += 0.25f;
        var projectile = Instantiate(AttackProjectile, position, Quaternion.identity);
        var projectileScript = projectile.GetComponent<FrostboltController>();
        projectileScript.SetDirection(_isFacingRight);
    }

    void OnAnimationAttackEnd()
    {
        _animator.Play(_isMoving ? ANIM_WALK : ANIM_IDLE);
        _isCasting = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
