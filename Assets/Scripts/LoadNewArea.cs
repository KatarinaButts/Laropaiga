using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string levelToLoad;
    public string exitPoint;

    private PlayerController thePlayer;
    
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        //Empty
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //save information about this scene
        GameState.SaveSceneVariables(SceneManager.GetActiveScene().name);

        if (collision.gameObject.gameObject.GetComponent<PlayerController>()) {
            SceneManager.LoadScene(levelToLoad);
            thePlayer.startPoint = exitPoint;
        }
        //load information about the next scene
        //GameState.LoadSceneVariables(levelToLoad);



    }
}
