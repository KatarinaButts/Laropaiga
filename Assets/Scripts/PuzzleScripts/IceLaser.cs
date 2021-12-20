using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaser : Interactable
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite IceLaserUpSprite;
    [SerializeField]
    private Sprite IceLaserDownSprite;
    [SerializeField]
    private Sprite IceLaserLeftSprite;
    [SerializeField]
    private Sprite IceLaserRightSprite;
    [SerializeField]
    private Sprite IceLaserActivatedUpSprite;
    [SerializeField]
    private Sprite IceLaserActivatedDownSprite;
    [SerializeField]
    private Sprite IceLaserActivatedLeftSprite;
    [SerializeField]
    private Sprite IceLaserActivatedRightSprite;
    [SerializeField]
    private GameObject laserPrefab;

    Rigidbody2D rigidbody2d;

    private Vector2 laserDirection = new Vector2(0.0f, -1.0f);
    private Vector2 offset = new Vector2(0.0f, 0.0f);
    bool activated = false;
    bool betweenLaunch = false;
    float timeBetweenLaunch = 2.0f;
    float launchTimer;
    float laserSpeed = 175.0f;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

        void Update()
    {
        
        if(activated == true)
        {
            if (betweenLaunch == true)
            {
                launchTimer -= Time.deltaTime;
                if (launchTimer < 0)
                {
                    betweenLaunch = false;
                }
            }
            else
            {
                Launch();
                betweenLaunch = true;
                launchTimer = timeBetweenLaunch;

            }
        }
        else
        {
            if (betweenLaunch == true)
            {
                launchTimer -= Time.deltaTime;
                if (launchTimer < 0)
                {
                    betweenLaunch = false;
                }
            }
        }
    }

    public override void Interact(Transform PlayerTransform)
    {
        base.Interact(PlayerTransform);

        ChangeSprite();
    }

    void ChangeSprite()
    {
        if (renderer.sprite.name == IceLaserUpSprite.name)
        {
            renderer.sprite = IceLaserActivatedUpSprite;
            offset = new Vector2(0.0f, 1.0f);
            laserDirection = new Vector2(0.0f, 1.0f);
            activated = true;
        }
        else if (renderer.sprite.name == IceLaserDownSprite.name)
        {
            renderer.sprite = IceLaserActivatedDownSprite;
            offset = new Vector2(0, -1.0f);
            laserDirection = new Vector2(0.0f, -1.0f);
            activated = true;
            //Launch();
        }
        else if (renderer.sprite.name == IceLaserLeftSprite.name)
        {
            renderer.sprite = IceLaserActivatedLeftSprite;
            offset = new Vector2(-1.0f, 0.0f);
            laserDirection = new Vector2(-1.0f, 0.0f);
            activated = true;
        }
        else if (renderer.sprite.name == IceLaserRightSprite.name) 
        {
            renderer.sprite = IceLaserActivatedRightSprite;
            offset = new Vector2(1.0f, 0.0f);
            laserDirection = new Vector2(1.0f, 0.0f);
            activated = true;
        }
        else if(renderer.sprite.name == IceLaserActivatedUpSprite.name)
        {
            renderer.sprite = IceLaserUpSprite;
            activated = false;
        }
        else if (renderer.sprite.name == IceLaserActivatedDownSprite.name)
        {
            renderer.sprite = IceLaserDownSprite;
            activated = false;
        }
        else if (renderer.sprite.name == IceLaserActivatedLeftSprite.name)
        {
            renderer.sprite = IceLaserLeftSprite;
            activated = false;
        }
        else if (renderer.sprite.name == IceLaserActivatedRightSprite.name)
        {
            renderer.sprite = IceLaserRightSprite;
            activated = false;
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(laserPrefab, rigidbody2d.position + offset, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(laserDirection, laserSpeed);
    }
}
