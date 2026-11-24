using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private bool _canAttack = true;
    [SerializeField] private GameObject _attack;
    [SerializeField] private float _speed = 1;
    private Animator _animator;
    private bool stop = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        stop = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_player.transform.position, gameObject.transform.position) > 2f && !stop)
        {
            _animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                _player.transform.position, 
                5f *_speed* Time.deltaTime
            );
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            if (_canAttack)
            {
                StartCoroutine(AttackDelay());
            }
        }
        if (transform.position.x != _player.transform.position.x || transform.position.y != _player.transform.position.y)
        {
                   

            if (transform.position.x > _player.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

        }

    }

    IEnumerator AttackDelay()
    {
        stop = true;
        _canAttack = false;
        _animator.SetTrigger("Attack0");
        yield return new WaitForSeconds(0.7f);
        _attack.SetActive(true);
        yield return new WaitForSeconds(1f);
        _attack.SetActive(false);
        stop = false;
        yield return new WaitForSeconds(2f);
        _canAttack = true;
        
    }
}
