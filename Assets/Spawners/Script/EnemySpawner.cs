using System;
using Scrips.Ennemies;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Scrips.Characters
{
    public class EnemySpawner : MonoBehaviour
    {
        private Renderer myRenderer;
        [SerializeField] GameObject enemies;
        [SerializeField] float cooldownDuration = 5;
        [SerializeField] int maxEnemies = 3;
        [SerializeField] int defaultEnemies = 3;
        private ObjectPool<GameObject> _pool;
        private float _spawnCooldown = 0;
        private int _activeEnemies = 0;
        


        void Awake()
        {
            myRenderer = GetComponent<Renderer>();
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,
                defaultCapacity: defaultEnemies,
                maxSize: maxEnemies
            );
        }

        private void Update()
        {
            
            if (myRenderer.isVisible && _activeEnemies < maxEnemies)
            {
                if (Time.time > _spawnCooldown)
                {
                    _pool.Get();
                    _spawnCooldown = Time.time + cooldownDuration;
                }
            }
        }
        

        private GameObject CreateItem()
        {
            GameObject pooledObject = Instantiate(enemies);
            pooledObject.name = enemies.name;
       
            
            
            pooledObject.SetActive(false);
            return pooledObject;
        }

        // Called when an item is taken from the pool.
        private void OnGet(GameObject pooledObject)
        {
            _activeEnemies++;
            EnemyCharacterStats instance = pooledObject.GetComponent<EnemyCharacterStats>();
            if (instance != null)
            {
                instance.Stats.SetSpawner(this);
               
            }
            pooledObject.transform.position = transform.position + new Vector3(Random.Range(1.5f,2.5f), Random.Range(1.5f,2.5f), 0);
            pooledObject.GetComponent<SpriteRenderer>().color = Color.white;
            if(pooledObject.GetComponent<MonsterMouvment>() != null)
                pooledObject.GetComponent<MonsterMouvment>().enabled = true;
            if(pooledObject.GetComponent<RangeMonster>() != null)
                pooledObject.GetComponent<RangeMonster>().enabled = true;
            pooledObject.GetComponent<DetectEnnemy>().enabled = true;
            pooledObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            pooledObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            pooledObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        private void OnRelease(GameObject pooledObject)
        {
            _activeEnemies--;
            pooledObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(GameObject pooledObject)
        {
            Destroy(pooledObject);
        }

        private System.Collections.IEnumerator ReturnAfter(GameObject pooledObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            // Give it back to the pool.
            _pool.Release(pooledObject);
        }
        public void ReturnEnemyToPool(GameObject enemy)
        {
            _pool.Release(enemy);
        }
    }
    
}