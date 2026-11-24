
using System.Collections;
using Scrips.Characters;
using UnityEngine;

namespace Scrips.Ennemies
{
    public class MonsterMouvment : MonoBehaviour
    {
        public float speed=3;
        public GameObject hero;
        private bool _canAttack = false;
        private bool _canMove = true;
        private bool _delay = false;
        private bool _isTouching = false;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private bool die = false;
        private bool stop = true;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = hero.GetComponent<SpriteRenderer>();
            
        }

        private void OnEnable()
        {
         _canAttack = false;
         _canMove = true;
         _delay = false;
         _isTouching = false;
         die = false;
         stop = true;
        }
        // Update is called once per frame
        void Update()
        {
   
                if (_canAttack && _isTouching && !_delay&&GetComponent<SpriteRenderer>().color != Color.black)
                {
                    if (name == "Skeleton")
                    {
                        if (Random.value > 0.5f)
                        {
                            animator.SetTrigger("Attack0");
                        }
                        else
                        {
                            animator.SetTrigger("Attack1"); // On appelle la methode attaque dans l'animation
                        }
                    }
                    else
                    {
                        animator.SetTrigger("Attack0");
                    }

                    StartCoroutine(AttackDelay());
 
                }
                if (_isTouching)
                {
                    _isTouching = false;
                    _canAttack = false;
                    _canMove = true;
                }
                if (_canMove && stop)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hero.transform.position,
                        speed * Time.deltaTime);
                        animator.SetBool("IsWalking", true);
                }
                else
                {
                        animator.SetBool("IsWalking", false);
                }
                if (transform.position.x != hero.transform.position.x || transform.position.y != hero.transform.position.y)
                {
                   

                    if (transform.position.x > hero.transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }

                }
                else
                {
                    animator.SetBool("isWalking", false);
                }

        }

        public void Attaque()
        {
            if (!Combat.invincible)
            {
                PlayerStats.SetCurrentHealth(PlayerStats.GetCurrentHealth() -
                                             GetComponent<EnemyCharacterStats>().Stats.GetAttackDamage());
                Combat player = hero.gameObject.GetComponent<Combat>();
                player.OnHitRender(die);
                if (PlayerStats.GetCurrentHealth() <= 0)
                {
                    die = true;
                }
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == hero)
            {
                _isTouching = true;
                if(!_delay)
                    _canAttack = true;
                _canMove = false;
       
            }
        }
        

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject == hero)
            {
                _isTouching = true;
                if(!_delay)
                    _canAttack = true;
                _canMove = false;
       
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject == hero)
            {
                _isTouching = false;
                _canAttack = false;
                _canMove = true;
       
            }
 
        }
        
        IEnumerator AttackDelay()
        {
            var enemyStats = GetComponent<EnemyCharacterStats>().Stats;
            _canAttack = false;
            _delay = true;
            
            yield return new WaitForSeconds(enemyStats.GetAttackDelay());
            _delay = false;
            
            if (_isTouching)
                _canAttack = true;
        }

        public void StopMovement()
        {
            stop = !stop;
        }
    }
}
