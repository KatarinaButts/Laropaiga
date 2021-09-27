using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAIController : MonoBehaviour
{
    float speed;

    bool vertical;
    bool broken;
    int direction;

    int count;

    float changeTime;
    float timer;

    Animator animator;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2.0f;
        vertical = false;
        broken = true;
        direction = 1;

        count = 0;

        changeTime = 1.0f;
        timer = changeTime;

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0 && (count % 2 == 0))
        {
            timer = changeTime;
            vertical = !(vertical);
            direction = -direction;
            count += 1;
        }
        else if(timer < 0)
        {
            timer = changeTime;
            vertical = !(vertical);
            //direction = direction;
            count = 0;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;

        if(vertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2D.MovePosition(position);
    }

    //test function, can remove later
     void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
    }

}
