using System;
using System.Collections;
using HP_System;
using Scrips.Ennemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scrips.Characters
{
    public class movment : MonoBehaviour
    {
        private InputAction mouseClic;
        private InputAction mousePos;
        private Camera mainCamera;
        private Vector2 targetPos;
        private Vector2 ennemyPos;
        [SerializeField] private float speed = 5;


        private float currentspeed;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        Combat combat;
        Vector2 ScreenPos;
        Vector3 mouseScreenPos;
        [HideInInspector] public GameObject targetEnemy;
        private bool noDoubleClick = false;

        private void Start()
        {
            targetPos = transform.position;
            mainCamera = Camera.main;
            combat = GetComponent<Combat>();

            // On crée les actions et on leur dit quoi écouter (le "binding")
            mouseClic = new InputAction(binding: "<Mouse>/leftButton");
            mousePos = new InputAction(binding: "<Mouse>/position");

            // Sans ça, les actions n'écoutent rien
            mouseClic.Enable();
            mousePos.Enable();

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            targetEnemy = null;
        }

        private void OnEnable()
        {
            targetPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            
            currentspeed = speed * Time.deltaTime;

            if (mouseClic.triggered)
            {
                if (noDoubleClick)
                    noDoubleClick = false;
                else
                {
                    Vector2 checkIfEnnemy;
                    ScreenPos = mousePos.ReadValue<Vector2>();
                    mouseScreenPos = new Vector3(ScreenPos.x, ScreenPos.y, 0);
                    mouseScreenPos.z = mainCamera.transform.localPosition.z * -1;
                    checkIfEnnemy = mainCamera.ScreenToWorldPoint(mouseScreenPos);
                    if (targetEnemy != null && !targetEnemy.GetComponent<Collider2D>().OverlapPoint(checkIfEnnemy))
                    {
                        targetEnemy = null;
                        StartCoroutine(UnStunDelay());
                    }
                    else if (targetEnemy == null)
                    {
                        targetPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
                    }
                }
            }

            if (targetEnemy != null)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, targetEnemy.transform.position,
                    currentspeed);
            }
            else
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, currentspeed);
            }

            if (targetEnemy == null)
            {
                if (transform.position.x != targetPos.x || transform.position.y != targetPos.y)
                {
                    if (transform.position.x > targetPos.x)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
            else
            {
                if (transform.position.x != targetEnemy.transform.position.x ||
                    transform.position.y != targetEnemy.transform.position.y)
                {
                    if (transform.position.x > targetEnemy.transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }

            if (Vector2.Distance(targetPos, transform.position) <= 0.5f)
            {
                targetPos = transform.position;
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
            }

            if (targetEnemy != null && targetEnemy.name == "EnnemiesSpawner")
            {
                if (Vector2.Distance(gameObject.transform.position, targetEnemy.transform.position) <= 1.8f)
                {
                    if (Combat._canAttack)
                    {
                        combat.Attack(targetEnemy);
                    }

                    targetPos = transform.position;
                    targetEnemy = null;
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    animator.SetBool("isWalking", true);
                }
            }
            else if (targetEnemy != null && targetEnemy.name == "Boss")
            {
                if (Vector2.Distance(gameObject.transform.position, targetEnemy.transform.position) <= 2f)
                {
                    if (Combat._canAttack)
                    {
                        combat.Attack(targetEnemy);
                    }

                    targetPos = transform.position;
                    targetEnemy = null;
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    animator.SetBool("isWalking", true);
                }
            }
            else if (targetEnemy != null)
            {
                if (Vector2.Distance(gameObject.transform.position, targetEnemy.transform.position) <= 1.2f)
                {
                    if (Combat._canAttack)
                    {
                        combat.Attack(targetEnemy);
                    }

                    targetEnemy = null;
                    targetPos = transform.position;
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    animator.SetBool("isWalking", true);
                }
            }
        }

        public void SetEnnemyTarget(GameObject target)
        {
            targetEnemy = target;
            noDoubleClick = true;
        }


        IEnumerator UnStunDelay()
        {
            yield return new WaitForSeconds(0.2f);
            combat.UnStun();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (targetEnemy != null)
            {
                if (other.gameObject != targetEnemy)
                {
                    targetEnemy = null;
                }
            }
        }
    }
}