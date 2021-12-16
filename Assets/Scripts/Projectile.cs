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
            e.Fix();
            Destroy(gameObject);
        }
        if (i != null)
        {
            Vector2 prevRigidBody2dPos = rigidbody2d.position;
            Vector2 bounceLaserPos = i.BounceLaser(rigidbody2d.position, laserDir, boxCollider.size);

            //temp fix
            char dir = i.getNewLaserDir();

            //boxCollider.size

            if (bounceLaserPos == rigidbody2d.position)
            {
                Debug.Log("Don't Bounce");
                //don't bounce
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Bounce");
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
                        Debug.Log("Default to down");
                        Launch(new Vector2(0.0f, -1.0f), projectileForce);
                        break;
                }

                //change direction based on the new position of the rigidbody2d.position
                //below is wrong, using chars as a temp fix above
                /*
                if(rigidbody2d.position.x < prevRigidBody2dPos.x && rigidbody2d.position.y > prevRigidBody2dPos.y)
                {
                    Debug.Log("LaserBeam moved North");
                    //for now just destroy, later change direction of force
                    Destroy(gameObject);
                    //Launch()
                }
                else if(rigidbody2d.position.x > prevRigidBody2dPos.x && rigidbody2d.position.y < prevRigidBody2dPos.y) {
                    Debug.Log("LaserBeam moved South");
                    //for now just destroy, later change direction of force
                    Destroy(gameObject);
                }
                else if(rigidbody2d.position.x < prevRigidBody2dPos.x && rigidbody2d.position.y < prevRigidBody2dPos.y)
                {
                    Debug.Log("LaserBeam moved West");
                    //for now just destroy, later change direction of force
                    Destroy(gameObject);
                }
                else if(rigidbody2d.position.x > prevRigidBody2dPos.x && rigidbody2d.position.y > prevRigidBody2dPos.y)
                {
                    Debug.Log("LaserBeam moved East");
                    //for now just destroy, later change direction of force
                    Destroy(gameObject);
                }
                */

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

        Debug.Log("Projectile collided with " + other.gameObject);
    }
}
