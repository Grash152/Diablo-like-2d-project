using System;
using System.Collections;
using Scrips.Characters;
using Scrips.Ennemies;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class BossScript : MonoBehaviour
{
    [SerializeField] private AudioClip EntranceMusic;
    [SerializeField] private AudioClip LightningImpact;
    [SerializeField] private AudioClip FarAway;
    [SerializeField] private AudioClip Static;
    [SerializeField] private AudioClip Strike;
    [SerializeField] private AudioClip Shoot;
    [SerializeField] private AudioClip Explosion1;
    [SerializeField] private AudioClip Explosion2;
    [SerializeField] private AudioClip Death;
    [SerializeField] private AnimationClip Entrance;
    [SerializeField] private GameObject Fog;
    [SerializeField] private GameObject NonBossMusic;
    [SerializeField] private GameObject Rain;
    [SerializeField] private GameObject Spawn;
    [SerializeField] private GameObject StrikeDown;
    [SerializeField] private GameObject Clouds;

    private SpriteRenderer sr;
    AudioSource audioSource;
    private Animator anim;
    private BossPattern move;
    private GameObject[] Enemies;
    private Animator anim2;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BossPattern>().enabled = false;
        StrikeDown.SetActive(true);
        Spawn.SetActive(true);
        Rain.SetActive(true);
        Fog.SetActive(true);
        Clouds.SetActive(true);
        NonBossMusic.SetActive(false);
        move = GetComponent<BossPattern>();
        move.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        StartCoroutine(PlayIntro());
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var monster in Enemies)
        {
            if (monster != gameObject)
            {
                if(monster.gameObject.GetComponent<MonsterMouvment>() != null)
                    monster.GetComponent<MonsterMouvment>().enabled = false;
                if(monster.gameObject.GetComponent<EnemySpawner>() != null)
                    monster.GetComponent<EnemySpawner>().enabled = false;
                if (monster.gameObject.name == "Skeleton")
                {
                    anim2 = monster.GetComponent<Animator>();
                    anim2.SetBool("IsWalking", false);
                }
                if (monster.gameObject.GetComponent<RangeMonster>() != null)
                {
                    monster.gameObject.GetComponent<RangeMonster>().enabled = false;
                }
                
                    
            }
        }
   
    }

    private void OnDisable()
    {
        StrikeDown.SetActive(false);
        Spawn.SetActive(false);
        Rain.SetActive(false);
        Fog.SetActive(false);
        Clouds.SetActive(false);
        NonBossMusic.SetActive(false);
    }

 
    
 

    IEnumerator PlayIntro()
    {
        audioSource.PlayOneShot(FarAway);
        yield return new WaitForSeconds(FarAway.length -1f);
        audioSource.PlayOneShot(EntranceMusic);
        yield return new WaitForSeconds(20f);
        audioSource.PlayOneShot(Static);
        yield return new WaitForSeconds(1f);
        Spawn.SetActive(false);
        sr.enabled = true;
        anim.enabled = true;
        yield return new WaitForSeconds(Entrance.length+5f);
        move.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void PlayDeath()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
    }
    public void Explosion()
    {
        audioSource.PlayOneShot(Explosion1);
        audioSource.PlayOneShot(Explosion2);
        GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 100);
        foreach (Collider2D col in hitColliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Sleep(col.gameObject));
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 Direction = (col.transform.position - transform.position).normalized;
                    rb.AddForce(Direction * 10, ForceMode2D.Impulse);
                    anim2 = col.GetComponent<Animator>();
                    anim2.SetTrigger("Hit");
                }
            }

        }
        

        foreach (var monster in Enemies)
        {
            if (monster != this.gameObject)
            {
                Destroy(monster);
               
            }
        }
    }
    
    IEnumerator Sleep(GameObject player)
    {
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponent<movment>().enabled = false;
        player.GetComponent<Combat>().enabled = false;
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<movment>().enabled = true;
        player.GetComponent<Combat>().enabled = true;
    }

    public void PlayStrike()
    {
        audioSource.PlayOneShot(Strike);
        audioSource.PlayOneShot(LightningImpact);
    }


  
}
