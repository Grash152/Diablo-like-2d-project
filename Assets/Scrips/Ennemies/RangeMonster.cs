using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class RangeMonster : MonoBehaviour
{
    private float attaqueDistance = 6;
    private float speed=3;
    private float fleeDuration = 6;
    private float nMouveDuration = 10;
    private float fleeDistance = 4;
    private float rateOfFire = 2;

    [SerializeField] private float projectileTime = 3f;
    [SerializeField] private GameObject bullet; 
    [SerializeField] private GameObject hero;
    private bool _canAttaque = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ObjectPool<GameObject> bulletPool;
    private bool stop = true;

    private void Awake()
    {
        bulletPool = new ObjectPool<GameObject>
        (createFunc: CreateItem,
        actionOnGet: OnGet,
        actionOnRelease: OnRelease,
        actionOnDestroy: OnDestroyItem,
        collectionCheck: true,
        defaultCapacity: 3,
        maxSize: 3);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = hero.GetComponent<SpriteRenderer>();
        _canAttaque = true;
    }


    void Update()
    {
        
        if (stop&&GetComponent<SpriteRenderer>().color != Color.black)
        {
            if (Vector2.Distance(hero.transform.position, transform.position) < fleeDistance && fleeDuration > 0)
            {
                MouvingAway();
                fleeDuration -= Time.deltaTime;
            }
            else if (Vector2.Distance(hero.transform.position, transform.position) > attaqueDistance)
            {
                MouvingToward();
            }
            else if (Vector2.Distance(hero.transform.position, transform.position) < attaqueDistance)
            {
                if (_canAttaque)
                {
                    animator.SetTrigger("Attack0"); // On appelle la methode attaque dans l'animation
                    StartCoroutine(chrono());
                }

                nMouveDuration -= Time.deltaTime;
            }
        }

        if (nMouveDuration < 0)
        {
            fleeDuration = 5;
            nMouveDuration = 5;
        }
    }
    void MouvingAway()
    {
        Vector2 fleeDirection = (transform.position-hero.transform.position).normalized;
        transform.position += (Vector3)fleeDirection*speed*Time.deltaTime;
        
    }
    void MouvingToward()
    {
        transform.position=Vector2.MoveTowards(transform.position,hero.transform.position,speed*Time.deltaTime);
        
    }
    void attaque()
    {
        Vector2 direction = (hero.transform.position-transform.position).normalized;
        GameObject projectile = bulletPool.Get();
        projectile.transform.position = transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        projectile.transform.rotation = Quaternion.Euler(0, 0,angle);
        StartCoroutine(ReturnAfter(projectile,projectileTime));
    }
    IEnumerator chrono()
    {
        _canAttaque = false;
        yield return new WaitForSeconds(rateOfFire);
        _canAttaque = true;
   
    }
    private IEnumerator ReturnAfter(GameObject pooledObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bulletPool.Release(pooledObject);
    }
    private GameObject CreateItem()
    {
        GameObject projectile = Instantiate(bullet);
        projectile.name = "bullet";
        projectile.SetActive(false);
        return projectile;
    }
    private void OnGet(GameObject projectile)
    {
        
        Projectilelit instance = projectile.GetComponent<Projectilelit>();
        if(instance != null)
            instance.SetSpawner(this);
        projectile.transform.position = transform.position;
        projectile.SetActive(true);
    }
    private void OnRelease(GameObject projectile)
    {
        projectile.SetActive(false);
    }
    private void OnDestroyItem(GameObject projectile)
    {
        Destroy(projectile);
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void StopMovement()
    {
        stop = !stop;
    }
}

