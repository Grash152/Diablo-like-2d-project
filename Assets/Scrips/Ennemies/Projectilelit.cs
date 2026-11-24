using System;
using System.Collections;
using Scrips.Characters;
using UnityEngine;

public class Projectilelit : MonoBehaviour
{
    private float speed = 5f;
    private SpriteRenderer spriteRenderer;
    private bool die = false;
    private RangeMonster _spawner;
    private Animator animator;
   

    void Update()
    {
        transform.Translate(Vector2.up*speed*Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag=="Player")
        {
            spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            GameObject hero = collision.gameObject;
            
            PlayerStats.SetCurrentHealth(PlayerStats.GetCurrentHealth() - 5);
            Combat player = collision.gameObject.GetComponent<Combat>();
            player.OnHitRender(die);
            if (PlayerStats.GetCurrentHealth() <= 0)
            {
                die = true;
            }
            if(_spawner !=null) 
                _spawner.ReturnToPool(gameObject);
            else
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void SetSpawner(RangeMonster spawner)
    {
        _spawner = spawner;
            
    }
}
