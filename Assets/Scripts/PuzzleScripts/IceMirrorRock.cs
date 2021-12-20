using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMirrorRock : Interactable
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite IceMirrorRock1Sprite;
    [SerializeField]
    private Sprite IceMirrorRock2Sprite;
    [SerializeField]
    private Sprite IceMirrorRock3Sprite;
    [SerializeField]
    private Sprite IceMirrorRock4Sprite;
    [SerializeField]
    private BoxCollider2D boxCollider;

    Rigidbody2D rigidbody2d;

    Vector2 boxColliderOffset;
    Vector2 laserPoint1 = new Vector2(0.0f, -1.0f);
    Vector2 laserPoint2 = new Vector2(0.0f, -1.0f);

    char newLaserDir = '0';

    public char getNewLaserDir()
    {
        return newLaserDir;
    }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        boxColliderOffset = new Vector2(boxCollider.size.x / 2, boxCollider.size.y / 2);

        //Calculate laserPoints
        if (renderer.sprite.name == IceMirrorRock1Sprite.name)
        {
            //bottom and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock2Sprite.name)
        {
            //top and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock3Sprite.name)
        {
            //top and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock4Sprite.name)
        {
            //bottom and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
        }
        else
        {
            //Default to IceMirrorRock1
            Debug.Log("Laser Points Set Up Error for " + renderer.sprite.name);
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
    }


    public override void Interact(Transform PlayerTransform)
    {
        DictionaryManager dictManager = GameObject.FindObjectOfType<GameManager>().GetComponent<DictionaryManager>();
        string searchWord = "mawasu";   //used IceMirrorRock objects for rotation

        if(dictManager.GetActiveWord() != null && dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord))
        {
            base.Interact(PlayerTransform);
            //Debug.Log("Interacting with " + transform.name);

            ChangeSprite();
        }
        else
        {
            //description/tooltip UI box if the player does not know the dictionary word or is not currently using it
        }
    }

    void ChangeSprite()
    {
        //4, 1, 3, 2
        if(renderer.sprite.name == IceMirrorRock1Sprite.name)
        {
            renderer.sprite = IceMirrorRock3Sprite;

            //top and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock2Sprite.name)
        {
            renderer.sprite = IceMirrorRock4Sprite;

            //bottom and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if (renderer.sprite.name == IceMirrorRock3Sprite.name)
        {
            renderer.sprite = IceMirrorRock2Sprite;

            //top and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if (renderer.sprite.name == IceMirrorRock4Sprite.name)
        {
            renderer.sprite = IceMirrorRock1Sprite;

            //bottom and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
    }

    public Vector2 BounceLaser(Vector2 laserPos, Vector2 laserDir, Vector2 laserSize)
    {
        Vector2 newLaserPos = new Vector2 (laserPos.x, laserPos.y);

       if(laserDir.x == 0.0f && laserDir.y == 1.0f)
        {
            //Laser going Up
            //Checking to see if laser hit wall that can reflect up lasers (IceMirrorRock1 && IceMirrorRock4)
            if (renderer.sprite.name == IceMirrorRock1Sprite.name)
            {
                //reflect left
                newLaserPos.x = laserPoint2.x - laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'L';
            }
            else if(renderer.sprite.name == IceMirrorRock4Sprite.name)
            {
                //reflect right
                newLaserPos.x = laserPoint2.x + laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'R';
            }
        }
        else if(laserDir.x == 0.0f && laserDir.y == -1.0f)
        {
            //Laser going Down
            //Checking to see if laser hit wall that can reflect down lasers (IceMirrorRock2 && IceMirrorRock3)
            if (renderer.sprite.name == IceMirrorRock2Sprite.name)
            {
                //reflect right
                newLaserPos.x = laserPoint2.x + laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'R';
            }
            else if (renderer.sprite.name == IceMirrorRock3Sprite.name)
            {
                //reflect left
                newLaserPos.x = laserPoint2.x - laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'L';
            }
        }
        else if(laserDir.x == -1.0f && laserDir.y == 0.0f)
        {
            //Laser going Left
            //Checking to see if laser hit wall that can reflect left lasers (IceMirrorRock2 && IceMirrorRock4)
            if (renderer.sprite.name == IceMirrorRock2Sprite.name)
            {
                //reflect up
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y + laserSize.y;

                newLaserDir = 'U';
            }
            else if (renderer.sprite.name == IceMirrorRock4Sprite.name)
            {
                //reflect down
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y - laserSize.y;

                newLaserDir = 'D';
            }
        }
        else if(laserDir.x == 1.0f && laserDir.y == 0.0f)
        {
            //Laser going right
            //Checking to see if laser hit wall that can reflect right lasers (IceMirrorRock1 && IceMirrorRock3)
            if (renderer.sprite.name == IceMirrorRock1Sprite.name)
            {
                //reflect down
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y - laserSize.y;

                newLaserDir = 'D';
            }
            else if (renderer.sprite.name == IceMirrorRock3Sprite.name)
            {
                //reflect up
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y + laserSize.y;

                newLaserDir = 'U';
            }
        }
        return newLaserPos;
    }
}

