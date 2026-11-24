using System;
using System.Collections;
using Scrips.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    [SerializeField] private GameObject _boss;

    private Vector2 _initialSize;
    private Vector2 _initialOffset;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        _collider = GetComponent<BoxCollider2D>();
        _initialSize = _collider.size;
        _initialOffset = _collider.offset;
        _collider.enabled = false;
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        _collider = GetComponent<BoxCollider2D>();
        _initialSize = _collider.size;
        _initialOffset = _collider.offset;
        _collider.enabled = false;
        
        Attack();
    }

    private void Update()
    {
        if (_renderer == null)
        {
            print("AAAAAAAAAAAAA");
        }
        transform.position = _boss.transform.position;
        //_player =  GameObject.FindWithTag("Player");
        var playerInput = _player.GetComponent<PlayerInput>();
        
    }

    void Attack()
    {
        //_player = GameObject.FindWithTag("Player");

        Vector2 direction = _player.transform.position - transform.position;

        float distanceX = Mathf.Abs(direction.x);
        float distanceY = Mathf.Abs(direction.y);

        _renderer.enabled = true;
        _collider.enabled = true;
        if (distanceX > distanceY)
        {
            _renderer.flipY = false;
            if (direction.x < 0)
            {
                _renderer.flipX = true;
                _collider.size = new Vector2(0.6f, _collider.size.y);
                _collider.offset = new Vector2(-_collider.size.x/2, _collider.offset.y);
            }
            else
            {
                _renderer.flipX = false;
                _collider.size = new Vector2(0.6f, _collider.size.y);
                _collider.offset = new Vector2(_collider.size.x/2, _collider.offset.y);
            }

            _animator.SetTrigger("XAttack");
        }
        else
        {
            _renderer.flipX = false;
            if (direction.y > 0)
            {
                _renderer.flipY = true;
                _collider.size = new Vector2(0.6f, 0.4f);
                _collider.offset = new Vector2(_collider.offset.x, _collider.size.y/2);
            }
            else
            {
                _renderer.flipY = false;
                _collider.size = new Vector2(0.6f, 0.4f);
                _collider.offset = new Vector2(_collider.offset.x, -_collider.size.y/2);
            }

            _animator.SetTrigger("YAttack");
        }

        StartCoroutine(activeDelay());
    }

    IEnumerator activeDelay()
    {
        yield return new WaitForSeconds(1f);
        _collider.size = _initialSize;
        _collider.offset = _initialOffset;
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    void Attaque()
    {
            bool die = false;
            PlayerStats.SetCurrentHealth(PlayerStats.GetCurrentHealth() - 8);
            if (PlayerStats.GetCurrentHealth() <= 0)
            {
                die =  true;
            }
            Combat player = _player.gameObject.GetComponent<Combat>();
            player.OnHitRender(die);
    }

}


