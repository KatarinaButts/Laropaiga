using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaserInput : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite laserInputSprite;    //not currently in use because we do not need to revert the laserInput ToDo: change this later if needed or remove this parameter
    [SerializeField]
    private Sprite laserInputSpriteActivated;
    [SerializeField]
    private Sprite circleLaser;
    [SerializeField]
    private Sprite squareLaser;
    [SerializeField]
    private Sprite triangleLaser;
    [SerializeField]
    private GameObject icePillar;

    private bool activated;

    public bool getActivatedState()
    {
        return activated;
    }

    private void Awake()
    {
        activated = false;
    }

    void Start()
    {
        //Empty
    }

    void Update()
    {
        //Empty
    }

    public void CheckLaserInput(Vector2 laserDir, Sprite laserType)
    {
        Debug.Log("Checking Laser Input");
        //laser is going north
        if (laserDir.x == 0.0f && laserDir.y == 1.0f)
        {
            Debug.Log("Laser going north confirmed");

            //check to make sure it's the appropriate laserType for the laser input
            if (renderer.sprite.name == "IceLaserInputCircleDown" && laserType.name == circleLaser.name)
            {
                Debug.Log("activated laser");
                //activate LaserInput
                renderer.sprite = laserInputSpriteActivated;

                icePillar.GetComponent<IceMirrorPillar>().LaserInputActivated();
                activated = true;
            }
           else if (renderer.sprite.name == "IceLaserInputSquareDown" && laserType.name == squareLaser.name)
            {
                Debug.Log("activated laser");
                //activate LaserInput
                renderer.sprite = laserInputSpriteActivated;

                icePillar.GetComponent<IceMirrorPillar>().LaserInputActivated();
                activated = true;

            }
            else if (renderer.sprite.name == "IceLaserInputTriangleDown" && laserType.name == triangleLaser.name)
            {
                Debug.Log("activated laser");
                //activate LaserInput
                renderer.sprite = laserInputSpriteActivated;

                icePillar.GetComponent<IceMirrorPillar>().LaserInputActivated();
                activated = true;
            }
        }
    }
}
