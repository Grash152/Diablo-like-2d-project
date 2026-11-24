using Scrips.Characters;
using System.Collections;
using HP_System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scrips.Ennemies
{
    public class DetectEnnemy : MonoBehaviour
    {
    
        private SpriteRenderer spriteRenderer;
        private movment movement;
        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GameObject player = GameObject.FindWithTag("Player");
            movement = player.GetComponent<movment>();
        }
        

        private void OnMouseOver()
        {
            if (spriteRenderer.color == Color.white)
            {
                spriteRenderer.color = Color.green;
            }

        }

        private void OnMouseDown()
        {
                movement.SetEnnemyTarget(gameObject);
            
        }

        private void OnMouseExit()
        {
            if(spriteRenderer.color == Color.green)
                spriteRenderer.color = Color.white;
        }
        
        

     

  
    }
}
