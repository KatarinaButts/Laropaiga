using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D rigidbody2d;
    SpriteRenderer renderer;
    Vector2 laserDir;
    float projectileForce = 0.0f;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
        
    }

    public void Launch(Vector2 direction, float force)
    {
        laserDir = direction;
        projectileForce = force;
        rigidbody2d.AddForce(direction * force);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IceMirrorRock i = other.collider.GetComponent<IceMirrorRock>();
        IceLaserInput iceLaserInput = other.collider.GetComponent<IceLaserInput>();
       
        HumanAIController e = other.collider.GetComponent<HumanAIController>();
        if (e!= null)
        {
            Destroy(gameObject);
        }
        if (i != null)
        {
            Vector2 prevRigidBody2dPos = rigidbody2d.position;
            Vector2 bounceLaserPos = i.BounceLaser(rigidbody2d.position, laserDir, boxCollider.size);

            char dir = i.getNewLaserDir();

            if (bounceLaserPos == rigidbody2d.position)
            {
                //Don't bounce
                Destroy(gameObject);
            }
            else
            {
                //Bounce
                rigidbody2d.position = bounceLaserPos;

                rigidbody2d.velocity = Vector2.zero;

                switch (dir)
                {
                    case 'U':   //up
                        Launch(new Vector2(0.0f, 1.0f), projectileForce);
                        break;
                    case 'D':   //down
                        Launch(new Vector2(0.0f, -1.0f), projectileForce);
                        break;
                    case 'L':   //left
                        Launch(new Vector2(-1.0f, 0.0f), projectileForce);
                        break;
                    case 'R':   //right
                        Launch(new Vector2(1.0f, 0.0f), projectileForce);
                        break;
                    default:
                        Debug.Log("New Laser Dir failed");
                        //Default to down
                        Launch(new Vector2(0.0f, -1.0f), projectileForce);
                        break;
                }

            }
        }
        else if(iceLaserInput != null)
        {
            iceLaserInput.CheckLaserInput(laserDir, renderer.sprite);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
