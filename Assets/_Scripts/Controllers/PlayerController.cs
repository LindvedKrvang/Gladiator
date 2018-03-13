using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : NetworkBehaviour
{
    private readonly string INPUT_HORIZONTAL = "Horizontal2";
    private readonly string INPUT_VERTICAL = "Vertical2";
    private readonly string SPEED_ATTRIBUTE = "Speed";
    private readonly string ANIM_DIE = "Dead";
    private readonly string ANIM_ATTACK = "Attack";

    public Character CharacterDetails;

    [SyncVar(hook = "OnDirectionChanged")]
    private bool _directionIsLeft;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    private float _speed;
    private int _health;
    private Vector2 _movementDirection;
    private bool _canMove;

    public override void OnStartLocalPlayer()
    {
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = transform;
        GetComponent<SpriteRenderer>().material.color = Color.blue;
    }

    // Use this for initialization
    void Start ()
	{
	    _rb = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();
	    _sr = GetComponent<SpriteRenderer>();

        _animator.runtimeAnimatorController = CharacterDetails.AnimatorController;
	    _health = CharacterDetails.Health;
        _canMove = true;
	}

    public void SetCharacterDetails(Character character)
    {
        CharacterDetails = character;
    }


    void FixedUpdate ()
    {
        if (!isLocalPlayer) return;

        _rb.velocity = _movementDirection * _speed;
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (_canMove)
            _movementDirection = new Vector2(Input.GetAxisRaw(INPUT_HORIZONTAL), Input.GetAxisRaw(INPUT_VERTICAL))
                .normalized;
        else
            _movementDirection = Vector2.zero;

        _speed = Input.GetKey(KeyCode.LeftShift) ? CharacterDetails.RunSpeed : CharacterDetails.WalkSpeed;

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
            CmdChangeDirectionOnServer(true);
        } else if (_movementDirection.x > 0)
        {
            CmdChangeDirectionOnServer(false);
        }
    }

    [Command]
    private void CmdChangeDirectionOnServer(bool directionIsLeft)
    {
        _directionIsLeft = directionIsLeft;
    }

    
    private void OnDirectionChanged(bool directionIsLeft)
    {
        _sr.flipX = directionIsLeft;
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

    [Command]
    public void CmdAttack()
    {
        if (!isServer)
            return;

        var ability = CharacterDetails.Attack(!_sr.flipX, transform.position);
        NetworkServer.Spawn(ability);
    }
}
