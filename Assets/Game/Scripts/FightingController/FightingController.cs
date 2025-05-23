using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingController : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    private CharacterController characterController;
    private Animator animator;

    [Header("Player Fight")]
    public float attackCooldown = 0.5f;
    public int attackDamage = 5;
    public string[] attackAnimations = {"Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation"};
    public float dodgeDistance = 2f;
    public float attackRadius = 2.2f;
    public Transform[] opponents;
    private float lastAttackTime;

    [Header("Effects and sounds")]
    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;

    public AudioClip[] hitSounds;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.GiveFullHealth(currentHealth);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        PerformMovement();
        PerformDodgeFront();

        if(Input.GetKeyDown(KeyCode.K))
        {
            PerformAttack(0);
        }
        else if(Input.GetKeyDown(KeyCode.J))
        {
            PerformAttack(1);
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            PerformAttack(2);
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            PerformAttack(3);
        }
    }

    void PerformMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-verticalInput, 0f, horizontalInput);

        if(movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            if(horizontalInput > 0)
            {
                animator.SetBool("Walking", true);
            }
            else if(horizontalInput < 0)
            {
                animator.SetBool("Walking", true);
            }
            else if(verticalInput != 0)
            {
                animator.SetBool("Walking", true);
            } 
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        characterController.Move(movement * movementSpeed * Time.deltaTime); 
    }

    void PerformAttack(int attackIndex)
    {
        if(Time.time - lastAttackTime > attackCooldown)
        {
            animator.Play(attackAnimations[attackIndex]);

            int damage = attackDamage;
            Debug.Log("Performed attack"+(attackIndex+1)+"dealing" + damage+"damage");

            lastAttackTime = Time.time;

            //Loop through each opponent
            foreach(Transform opponent in opponents)
            {
                if(Vector3.Distance(transform.position, opponent.position) <= attackRadius)
                {
                    OpponentAI opponentAI = opponent.GetComponent<OpponentAI>();
                    if (opponentAI != null && opponentAI.gameObject.activeInHierarchy)
                    {
                        opponentAI.StartCoroutine(opponentAI.PlayHitDamageAnimation(attackDamage));
                    }
                }
            }
        }
        else{
            Debug.Log("Endlag");
        }
    }

    void PerformDodgeFront()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.Play("DodgeFrontAnimation");

            Vector3 dodgeDiretion = transform.forward * dodgeDistance;

            characterController.Move(dodgeDiretion);
        }
    }

    public IEnumerator PlayHitDamageAnimation(int takeDamage)
    {
        yield return new WaitForSeconds(0.5f);

        if (hitSounds != null && hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[randomIndex], transform.position);
        }

        currentHealth -= takeDamage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        animator.Play("HitDamageAnimation");
    } 

    void Die()
    {
        Debug.Log("Player died.");
    }

    public void Attack1Effect()
    {
        attack1Effect.Play();
    }
    public void Attack2Effect()
    {
        attack2Effect.Play();
    }
    public void Attack3Effect()
    {
        attack3Effect.Play();
    }
    public void Attack4Effect()
    {
        attack4Effect.Play();
    }
}
