using System;
using System.Collections;
using Scrips.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UseSpecialAttack : MonoBehaviour
{
    [FormerlySerializedAs("vizualRange")] [SerializeField] GameObject visualRange;
    private movment _movment;
    private PlayerInput _input;
    private bool _canAttack = true;
    private Animator _animator;
    private const float ManaCostPercent = 0.4f; //AJOUT MICHAEL 2026-09-07 12:33am
    
    void Start()
    {
        _movment = GetComponent<movment>();
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_input.actions["SpecialAttack"].WasPerformedThisFrame() && _canAttack)
        {
            if (ManaSystem.HasEnoughMana())
            {
                _movment.enabled = false;
                ManaSystem.UseMana(ManaCostPercent);
                StartCoroutine(Special());
            }
            else
            {
                Debug.Log("T'as pu de mana!!");
            }
        }
        
        IEnumerator Special()
        {
            _canAttack = false;
            _animator.SetTrigger("spAttack");
                
            yield return new WaitForSeconds(1.2f);
            
            visualRange.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            
            visualRange.SetActive(false);
            _canAttack = true;
            _movment.enabled = true;
            
        }
    }
}
