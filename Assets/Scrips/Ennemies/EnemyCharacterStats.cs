using Scrips.Ennemies;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

namespace Scrips.Characters
{
    public class EnemyCharacterStats : MonoBehaviour
    {
        public EnemyStats Stats= new EnemyStats();
        private Animator _animator;
        private SpriteRenderer spriteRenderer;
        private MonsterMouvment _mouvment;
        private bool _isDying = false;
        [SerializeField] private GameObject xpOrbs;
        
        void OnEnable()
        {
            _isDying = false;
            if (Stats != null)
            {
                Stats.SetCurrentHealth(Stats.GetMaxHealth());
            }
            _animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _mouvment = GetComponent<MonsterMouvment>();
        }

        public void TakeDamage(int damage)
        {
            if (_isDying) return;
            bool die = false;
            if (name == "Skeleton")
            {
                _animator.SetTrigger("Hit");
            }
            Stats.SetCurrentHealth(Stats.GetCurrentHealth() - damage);
            if(Stats.GetCurrentHealth() <= 0)
                {
                    Stats.SetCurrentHealth(0);
                    die = true;
                    _isDying = true;
                }
            StartCoroutine(HitAnimation(die));
        }

        IEnumerator HitAnimation(bool die)
        {
            Vector2 position=gameObject.transform.position;
            if(_mouvment != null && _mouvment.enabled)
            {
                _mouvment.enabled = false;
            }
            if (die)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GameObject.FindWithTag("Player").GetComponent<movment>().targetEnemy = null;
                gameObject.GetComponent<DetectEnnemy>().enabled = false;
                if(_mouvment != null)
                {
                    _mouvment.enabled = false;
                }
                if(gameObject.GetComponent<RangeMonster>() != null)
                {
                    gameObject.GetComponent<RangeMonster>().enabled = false;
                }

                if (name != "Boss")
                {
                    spriteRenderer.color = Color.black;
                }

                if (name == "Skeleton")
                {
                    _animator.SetTrigger("Die");
                }

                if (name == "Boss")
                {
                    _animator.SetTrigger("Die");
                    PlayerStats.SetCurrentHealth(PlayerStats.GetMaxHealth());
                    gameObject.GetComponent<BossPattern>().enabled = false;
                    
                    yield return new WaitForSeconds(6f);
                    gameObject.GetComponent<AudioSource>().enabled = false;
                    yield return new WaitForSeconds(3f);
                    SceneManager.LoadScene("OutroScene");
                }
            }
            else
                spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(1f);
            if(_mouvment != null)
            {
                _mouvment.enabled = true;
            }
            if (die && name != "Boss")
            {
                if (_mouvment != null)
                    _mouvment.enabled = false;
                SpawnOrbs(position);
                EnemySpawner spawner = Stats.GetSpawner();
                if (spawner != null)
                {
                    spawner.ReturnEnemyToPool(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
           
            
        }
        private void SpawnOrbs(Vector2 ennemiePos)
        {
            GameObject lastOrbs = Instantiate(xpOrbs);
            lastOrbs.GetComponent<orbsOfXP>().xpValue = Stats.GetxpValue();
            lastOrbs.transform.position = ennemiePos;
            lastOrbs.transform.localScale = Vector2.one * (Stats.GetxpValue() * 0.01f);
        }
    }
    [System.Serializable]
    public class EnemyStats
    {
        [SerializeField] string name = "Enemy Name";
        [SerializeField] int maxHealth = 10;
        [SerializeField] int attackDamage = 5;
        [SerializeField] int defense = 0;
        [SerializeField] float attackDelay = 3f;
        [SerializeField] int currentHealth = 10;
        [SerializeField] int xpValueE = 10;
        private EnemySpawner _spawner;

        public int GetxpValue()
        {
            return xpValueE;
        }
        public string GetName()
        {
            return name;
        }

        public void SetName(string value)
        {
            name = value;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetMaxHealth(int value)
        {
            maxHealth = value;
        }

        public int GetAttackDamage()
        {
            return attackDamage;
        }

        public void SetAttackDamage(int value)
        {
            attackDamage = value;
        }

        public int GetDefense()
        {
            return defense;
        }

        public void SetDefense(int value)
        {
            defense = value;
        }

        public float GetAttackDelay()
        {
            return attackDelay;
        }

        public void SetAttackDelay(float value)
        {
            attackDelay = value;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public void SetCurrentHealth(int value)
        {
            currentHealth = value;
        }

        public void SetSpawner(EnemySpawner spawner)
        {
            _spawner = spawner;
            
        }

        public EnemySpawner GetSpawner()
        {
            
            return _spawner;
        }
    }
}
