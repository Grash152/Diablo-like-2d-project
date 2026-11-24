using System;
using Scrips.Characters;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyStats = other.gameObject.GetComponent<EnemyCharacterStats>();
            enemyStats.TakeDamage(20);
        }
    }

    
}
