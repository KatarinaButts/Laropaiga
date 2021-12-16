using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget;
    [SerializeField]
    private float moveSpeed;

    private Vector3 targetPos;
    
    private static bool menuCameraExists;

    void Start()
    {
        if (!menuCameraExists)
        {
            menuCameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.position = targetPos;

    }
}
