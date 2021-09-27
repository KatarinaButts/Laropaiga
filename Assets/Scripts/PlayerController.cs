using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ToDo: Create and set up launch animator for player (WitchSprite for now)

    Animator animator;
    Rigidbody2D rigidbody2d;

    public GameObject projectilePrefab;
    public HealthBar healthBar;

    Vector2 lookDirection = new Vector2(1,0);

    float horizontal;
    float vertical;

    float speed;

    int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }

    bool isInvincible;
    float timeInvincible = 2.0f;
    float invincibleTimer;

    int level;
    int experienceToNextLevel;
    int currentExperience;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        speed = 3.0f;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        isInvincible = false;

        level = 1;
        currentExperience = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log(horizontal);

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position - Vector2.up * 0.4f/* + Vector2.up * 0.01f*/, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            
            
            if(hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if(character != null)
                {
                    character.DisplayDialog();
                }
                //Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            }
            //Interact();
        }
        

    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.fixedDeltaTime;
        position.y = position.y + speed * vertical * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if(isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void LevelUp()
    {
        if(level != 99) //level cap
        {
            level += 1;
            currentExperience = 0;
            //figure out  experienceToNextLevel  calculation
            //figure out  maxHealth  calculation
            //figure out defense calculation??
            //figure out attack calculation??
        }

    }

    void Interact()
    {
        //add, based on direction, check for an object in front of the player
        //object.Interact();


        //animator.SetTrigger("Interact");  //!!!once we actually have interact animations available
    }

    void Launch()
    {
        //!!!Edit > Project Setings > Input > axes > Fire1      to make it more compatible with other devices
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position/* + Vector2.up * 0.2f*/, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
    }
}
