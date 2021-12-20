using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLaserInput : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Sprite laserInputSprite;
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

    public void CheckLaserInput(Vector2 laserDir, Sprite laserType)
    {
        //Laser is going north
        if (laserDir.x == 0.0f && laserDir.y == 1.0f)
        {
            //Check to make sure it's the correct laserType for the laser input
            if (renderer.sprite.name == "IceLaserInputCircleDown" && laserType.name == circleLaser.name)
            {
                //activate LaserInput
                renderer.sprite = laserInputSpriteActivated;

                icePillar.GetComponent<IceMirrorPillar>().LaserInputActivated();
                activated = true;
            }
           else if (renderer.sprite.name == "IceLaserInputSquareDown" && laserType.name == squareLaser.name)
            {
                //activate LaserInput
                renderer.sprite = laserInputSpriteActivated;

                icePillar.GetComponent<IceMirrorPillar>().LaserInputActivated();
                activated = true;

            }
            else if (renderer.sprite.name == "IceLaserInputTriangleDown" && laserType.name == triangleLaser.name)
            {
                //activate LaserInput
                renderer.sprite = laserInputSpriteActivated;

                icePillar.GetComponent<IceMirrorPillar>().LaserInputActivated();
                activated = true;
            }
        }
    }
}
