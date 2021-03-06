using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;

    private static bool playerExists;   //playerExists is the same for all objects with PlayerController script
    public string startPoint;
    public string playerName;

    public GameObject projectilePrefab;
    public HealthBar healthBar;
    List<Item> inventory;

    public Vector2 lookDirection = new Vector2(1,0);

    float horizontal;
    float vertical;

    float speed;
    float steps;

    int maxHealth = 20;
    int currentHealth;
    int damage;
    int magicPoints;

    bool isInvincible;
    float timeInvincible = 2.0f;
    float invincibleTimer;

    bool allowMovement;

    int level;
    int experienceToNextLevel;
    int currentExperience;

    public string getName { get { return playerName; } }
    public int getLevel { get { return level; } }
    public float getSteps { get { return steps; } }
    public int getHealth { get { return currentHealth; } }
    public int getMaxHealth { get { return maxHealth; } }
    public int getDamage { get { return damage; } }
    public int getMP { get { return magicPoints; } }


    private void Awake()
    {
        playerName = this.name;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        speed = 4.0f;
        steps = 0.0f;
        damage = 5;
        magicPoints = 0;
        level = 1;
        currentExperience = 0;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        inventory = new List<Item>();

        isInvincible = false;

        allowMovement = true;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if (allowMovement) { 
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

            if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
               
            }
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        /* Add back into game once the word for throw has been added to the dictionary
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        */
        
        if(Input.GetKeyDown(KeyCode.E))
        {

            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position - Vector2.up * 0.4f, lookDirection, 1.5f, LayerMask.GetMask("Interactable"));

            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    //Debug.Log("We hit something with an Interactable script");
                    hit.collider.GetComponent<Interactable>().Interact(transform);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        Vector2 prevPlayerPos = new Vector2(position.x, position.y);

        position.x = position.x + speed * horizontal * Time.fixedDeltaTime;
        position.y = position.y + speed * vertical * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);

        Vector2 newPlayerPos = new Vector2(position.x, position.y);

        if(prevPlayerPos != newPlayerPos)
        {
            steps += 0.05f;
        }
    }

    public void setAllowMovement(bool allowToMove)
    {
        allowMovement = allowToMove;
        if(allowMovement == false)
        {
            horizontal = 0.0f;
            vertical = 0.0f;
            animator.SetFloat("Speed", 0);
        }
    }

    public void ResetSteps ()
    {
        steps = 0;
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
        }

    }

    //Test function for launching an item from the player
    /*
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
    }
    */
}
