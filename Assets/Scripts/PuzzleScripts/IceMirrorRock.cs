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

        //calculate laserPoints
        if (renderer.sprite.name == IceMirrorRock1Sprite.name)
        {
            //Debug.Log("***Laser Points Set Up for " + renderer.sprite.name  + "***");
            //bottom and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock2Sprite.name)
        {
            //Debug.Log("***Laser Points Set Up for " + renderer.sprite.name + "***");
            //top and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock3Sprite.name)
        {
            //Debug.Log("***Laser Points Set Up for " + renderer.sprite.name + "***");
            //top and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
        else if(renderer.sprite.name == IceMirrorRock4Sprite.name)
        {
            //Debug.Log("***Laser Points Set Up for " + renderer.sprite.name + "***");
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
        }
        else
        {
            //default to IceMirrorRock1
            Debug.Log("***Laser Points Set Up Error for " + renderer.sprite.name + "***");
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
        }
    }


    public override void Interact(Transform PlayerTransform)
    {
        DictionaryManager dictManager = GameObject.FindObjectOfType<GameManager>().GetComponent<DictionaryManager>();
        string searchWord = "mawasu";
        Debug.Log("dictManager.GetActiveWord().GetSearchWord().ToLower() = " + dictManager.GetActiveWord().GetSearchWord().ToLower());
        Debug.Log("active word: " + dictManager.GetActiveWord());
        Debug.Log("search word: " + dictManager.GetActiveWord().GetSearchWord());

        Debug.Log("dictManager.GetActiveWord() != null: " + (dictManager.GetActiveWord() != null));
        Debug.Log("dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord) = " + dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord));

        if(dictManager.GetActiveWord() != null && dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord))
        {
            base.Interact(PlayerTransform);
            Debug.Log("Interacting with " + transform.name);

            ChangeSprite();
        }
        else
        {
            //bring up a description/tooltip box
            Debug.Log("You don't know this word or it's not currently your active word");
        }
    }

    void ChangeSprite()
    {

        //4, 1, 3, 2
        Debug.Log("Changing Sprite");
        if(renderer.sprite.name == IceMirrorRock1Sprite.name)
        {
            renderer.sprite = IceMirrorRock3Sprite;

            Debug.Log("***LaserPointsChanged***");
            //top and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
            Debug.Log("LaserPoint1: " + laserPoint1.x + ", " + laserPoint1.y);
            Debug.Log("LaserPoint2: " + laserPoint2.x + ", " + laserPoint2.y);
        }
        else if(renderer.sprite.name == IceMirrorRock2Sprite.name)
        {
            renderer.sprite = IceMirrorRock4Sprite;

            Debug.Log("***LaserPointsChanged***");
            //bottom and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
            Debug.Log("LaserPoint1: " + laserPoint1.x + ", " + laserPoint1.y);
            Debug.Log("LaserPoint2: " + laserPoint2.x + ", " + laserPoint2.y);
        }
        else if (renderer.sprite.name == IceMirrorRock3Sprite.name)
        {
            renderer.sprite = IceMirrorRock2Sprite;

            Debug.Log("***LaserPointsChanged***");
            //top and right
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x + boxColliderOffset.x, rigidbody2d.position.y);
            Debug.Log("LaserPoint1: " + laserPoint1.x + ", " + laserPoint1.y);
            Debug.Log("LaserPoint2: " + laserPoint2.x + ", " + laserPoint2.y);
        }
        else if (renderer.sprite.name == IceMirrorRock4Sprite.name)
        {
            renderer.sprite = IceMirrorRock1Sprite;

            Debug.Log("***LaserPointsChanged***");
            //bottom and left
            laserPoint1 = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y - boxColliderOffset.y);
            laserPoint2 = new Vector2(rigidbody2d.position.x - boxColliderOffset.x, rigidbody2d.position.y);
            Debug.Log("LaserPoint1: " + laserPoint1.x + ", " + laserPoint1.y);
            Debug.Log("LaserPoint2: " + laserPoint2.x + ", " + laserPoint2.y);
        }
    }

    public Vector2 BounceLaser(Vector2 laserPos, Vector2 laserDir, Vector2 laserSize)
    {
        //Debug.Log("Bouncing Laser off Mirror Rock Test");
        //Debug.Log("LaserDir: " + laserDir.x + ", " + laserDir.y);
        Vector2 newLaserPos = new Vector2 (laserPos.x, laserPos.y);

        //Debug.Log("LaserPos: " + laserPos.x + ", " + laserPos.y);
        //Debug.Log("MirrorPoint1: " + laserPoint1.x + ", " + laserPoint1.y);
        //Debug.Log("MirrorPoint2: " + laserPoint2.x + ", " + laserPoint2.y);

       if(laserDir.x == 0.0f && laserDir.y == 1.0f)
        {
            Debug.Log("Laser Going Up");
            //Laser going Up
            //Checking to see if laser hit wall that can reflect up lasers (IceMirrorRock1 && IceMirrorRock4)
            if (renderer.sprite.name == IceMirrorRock1Sprite.name)
            {
                Debug.Log("Reflecting Left");
                //reflect left
                newLaserPos.x = laserPoint2.x - laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'L';
            }
            else if(renderer.sprite.name == IceMirrorRock4Sprite.name)
            {
                Debug.Log("Reflecting Right");
                //reflect right
                newLaserPos.x = laserPoint2.x + laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'R';
            }
        }
        else if(laserDir.x == 0.0f && laserDir.y == -1.0f)
        {
            Debug.Log("Laser Going Down");
            //Laser going Down
            //Checking to see if laser hit wall that can reflect down lasers (IceMirrorRock2 && IceMirrorRock3)
            if (renderer.sprite.name == IceMirrorRock2Sprite.name)
            {
                Debug.Log("Reflecting Right");
                //reflect right
                newLaserPos.x = laserPoint2.x + laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'R';
            }
            else if (renderer.sprite.name == IceMirrorRock3Sprite.name)
            {
                Debug.Log("Reflecting Left");
                //reflect left
                newLaserPos.x = laserPoint2.x - laserSize.x;
                newLaserPos.y = laserPoint2.y;

                newLaserDir = 'L';
            }
        }
        else if(laserDir.x == -1.0f && laserDir.y == 0.0f)
        {
            Debug.Log("Laser Going Left");
            //Laser going Left
            //Checking to see if laser hit wall that can reflect left lasers (IceMirrorRock2 && IceMirrorRock4)
            if (renderer.sprite.name == IceMirrorRock2Sprite.name)
            {
                Debug.Log("Reflecting Up");
                //reflect up
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y + laserSize.y;

                newLaserDir = 'U';
            }
            else if (renderer.sprite.name == IceMirrorRock4Sprite.name)
            {
                Debug.Log("Reflecting Down");
                //reflect down
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y - laserSize.y;

                newLaserDir = 'D';
            }
        }
        else if(laserDir.x == 1.0f && laserDir.y == 0.0f)
        {
            Debug.Log("Laser Going Right");
            //Laser going right
            //Checking to see if laser hit wall that can reflect right lasers (IceMirrorRock1 && IceMirrorRock3)
            if (renderer.sprite.name == IceMirrorRock1Sprite.name)
            {
                Debug.Log("Reflecting Down");
                //reflect down
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y - laserSize.y;

                newLaserDir = 'D';
            }
            else if (renderer.sprite.name == IceMirrorRock3Sprite.name)
            {
                Debug.Log("Reflecting Up");
                //reflect up
                newLaserPos.x = laserPoint1.x;
                newLaserPos.y = laserPoint1.y + laserSize.y;

                newLaserDir = 'U';
            }
        }

        /*
        if (Mathf.Approximately(laserPos.x, laserPoint1.x) && Mathf.Approximately(laserPos.y, laserPoint1.y))
        {
            Debug.Log("Bounced on LaserPoint1");
            Debug.Log("Setting newLaserPos to laserPoint2");

            newLaserPos = new Vector2(laserPoint2.x, laserPoint2.y);
        }
        else if (Mathf.Approximately(laserPos.x, laserPoint2.x) && Mathf.Approximately(laserPos.y, laserPoint2.y))
        {
            Debug.Log("Bounced on LaserPoint2");
            Debug.Log("Setting newLaserPos to laserPoint1");

            newLaserPos = new Vector2(laserPoint1.x, laserPoint1.y);

        }
        */
        Debug.Log("newLaserPos: " + newLaserPos.x + ", " + newLaserPos.y);

        return newLaserPos;
    }


    void Rotate()
    {

    }
}

