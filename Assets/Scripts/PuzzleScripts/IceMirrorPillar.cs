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

    public void LaserInputActivated()
    {
        inputAmount += 1;

        if(IceLaserInputArray.Length == inputAmount)
        {
            //destroy this pillar
            Destroy(gameObject);
        }
    }
}
