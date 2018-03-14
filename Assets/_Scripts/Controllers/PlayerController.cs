using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : NetworkBehaviour
{
    private const string INPUT_HORIZONTAL = "Horizontal";
    private const string INPUT_VERTICAL = "Vertical";
    private const string SPEED_ATTRIBUTE = "Speed";
    private const string ANIM_DIE = "Dead";
    private const string ANIM_ATTACK = "Attack";
    private const string ANIM_IDLE = "Idle";

    public Character CharacterDetails;
    public RectTransform HealthBar;
    public RectTransform HealthBarBackground;

    [SyncVar(hook = "OnDirectionChanged")]
    private bool _directionIsLeft;

    [SyncVar(hook = "OnChangeHealth")]
    private int _health;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    private float _speed;
    
    private Vector2 _movementDirection;
    private bool _canMove;
    private bool _isDead;

    private NetworkStartPosition[] _spawnPoints;

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
        _isDead = false;
        HealthBarBackground.sizeDelta = new Vector2(_health, HealthBarBackground.sizeDelta.y);

        if(isLocalPlayer)
            _spawnPoints = FindObjectsOfType<NetworkStartPosition>();
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

        if (Input.GetKeyDown(KeyCode.Space) && !_isDead)
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

    private void OnChangeHealth(int health)
    {
        HealthBar.sizeDelta = new Vector2(health, HealthBar.sizeDelta.y);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        _health -= amount;
        if (_health <= 0)
        {
            //TODO RKL: Implement death
            RpcDie();
        }
    }

    [ClientRpc]
    void RpcDie()
    {
        _isDead = true;
        _canMove = false;
        _animator.SetTrigger(ANIM_DIE);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (!isLocalPlayer) return;

        var spawnPoint = Vector2.zero;
        if (_spawnPoints != null && _spawnPoints.Length > 0)
        {
            spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
        }
        transform.position = spawnPoint;
        _canMove = true;
        _isDead = false;
        _animator.Play(ANIM_IDLE);
        CmdResetHealth();
        
    }

    [Command]
    void CmdResetHealth()
    {
        _health = CharacterDetails.Health;
    }
}
