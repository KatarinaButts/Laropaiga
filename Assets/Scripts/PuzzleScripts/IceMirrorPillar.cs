using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMirrorPillar : MonoBehaviour
{
    [SerializeField]
    GameObject[] IceLaserInputArray;
    int inputAmount;

    private void Awake()
    {
        inputAmount = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LaserInputActivated()
    {
        inputAmount += 1;

        if(IceLaserInputArray.Length == inputAmount)
        {
            //have new camera move from player location to this mirror pillar location
            //play animation showing pillar breaking into the ground

            //destroy this pillar
            Destroy(gameObject);
        }
    }
}
