using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController thePlayer;
    private CameraController theCamera;

    public Vector2 startDirection;

    public string pointName;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        if(thePlayer.startPoint == pointName)
        {
        thePlayer.transform.position = transform.position;
        thePlayer.lookDirection = startDirection;

        theCamera = FindObjectOfType<CameraController>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);

        Debug.Log("About to load scene variables from PlayerStartPoint with active scene as: " + SceneManager.GetActiveScene().name);
        
        //load information about the next scene
        GameState.LoadSceneVariables(SceneManager.GetActiveScene().name);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
