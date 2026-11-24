using System.Collections;
using HP_System;
using Scrips.Ennemies;
using Scrips.Menu;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scrips.Characters
{
    public class Combat : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        [SerializeField] private AudioSource gameMusic;
        [SerializeField] private DeathEffect deathEffect;
        [SerializeField] private DeathMenuButtons menu;
        public static bool _canAttack = true;
        private Animator animator;
        [SerializeField] Slider _healthBar;
        [SerializeField] Slider _cooldownBar;
        private float elapsedTime;
        public static bool invincible;
        private EnemyCharacterStats enemyStats;
        private GameObject stunTarget;
        movment _movment;
        private float baseHp;

        private void Start()
        {
            invincible = false;
            _cooldownBar.maxValue = PlayerStats.GetAttackDelay();
            _cooldownBar.minValue = 0;
            _cooldownBar.value = PlayerStats.GetAttackDelay();
            PlayerStats.SetCurrentHealth(PlayerStats.GetMaxHealth());
            animator = GetComponent<Animator>();
            _healthBar.maxValue = PlayerStats.GetMaxHealth();
            _healthBar.value = PlayerStats.GetCurrentHealth();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _movment = GetComponent<movment>();
            baseHp = _healthBar.maxValue;
        }

        void Update()
        {
            if (PlayerStats.GetCurrentHealth() <= 0)
            {
                StartCoroutine(Die());
            }

            UpdateHealth();
        }

        private void UpdateHealth()
        {
            _healthBar.value = PlayerStats.GetCurrentHealth();
            if (baseHp != PlayerStats.GetMaxHealth())
            {
                _healthBar.maxValue = PlayerStats.GetMaxHealth();
                baseHp = _healthBar.maxValue;
            }
            
        }

        IEnumerator Die()
        {
            animator.SetTrigger("death");
            GetComponent<movment>().enabled = false;

            yield return new WaitForSeconds(2);

            Time.timeScale = 0.0f;
            
            menu.gameObject.SetActive(true);
            
            gameMusic.Pause();
            deathEffect.StartDeathEffect();
        }

        IEnumerator CDBar()
        {
            elapsedTime = 0;
            while (elapsedTime < PlayerStats.GetAttackDelay())
            {
                elapsedTime += Time.deltaTime;
                _cooldownBar.value = elapsedTime;
                yield return null;
            }
        }
        IEnumerator AttackDelay()
        {
            _canAttack = false;
			
            yield return new WaitForSeconds(PlayerStats.GetAttackDelay());
            
            _canAttack = true;

        }

        IEnumerator EnemyStun()
        {
            if (stunTarget != null)
            {
                stunTarget.GetComponent<MonsterMouvment>().enabled = false;
            }

            yield return new WaitForSeconds(1.2f);
            if (stunTarget != null)
            {
                stunTarget.GetComponent<MonsterMouvment>().enabled = true;
            }
        }
        IEnumerator FlyingEnemyStun()
        {
            if (stunTarget != null)
            {
                stunTarget.GetComponent<RangeMonster>().enabled = false;
            }

            yield return new WaitForSeconds(1.2f);
            if (stunTarget != null)
            {
                stunTarget.GetComponent<RangeMonster>().enabled = true;
            }
            
        }

        public void UnStun()
        {
            if(stunTarget != null && stunTarget.GetComponent<MonsterMouvment>() != null)
                stunTarget.GetComponent<MonsterMouvment>().enabled = true;
        }
        public void Attack(GameObject target)
        {
            if (target != null && _canAttack)
            {
                stunTarget = target;
                enemyStats = target.GetComponent<EnemyCharacterStats>();
                if(stunTarget != null && stunTarget.GetComponent<MonsterMouvment>()!=null)
                    StartCoroutine(EnemyStun());
                else if (stunTarget != null && stunTarget.GetComponent<RangeMonster>()!=null)
                    StartCoroutine(FlyingEnemyStun());
                animator.SetTrigger("attack"); // On appelle la methode attaque dans l'animation
                StartCoroutine(CDBar());
                StartCoroutine(AttackDelay());
            }
        }

        public void Attaque()
        {
            if(enemyStats != null)
                enemyStats.TakeDamage(PlayerStats.GetAttackDamage());
        }
        public void OnHitRender(bool die)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(HeroAttackAnim(die));
        }

        IEnumerator HeroAttackAnim(bool die)
        {
            
            spriteRenderer.color = die ? Color.black : Color.red;
            invincible = true;
            yield return new WaitForSeconds(1f);

            spriteRenderer.color = Color.white;
            invincible = false;
        }
    }
}