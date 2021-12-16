using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Forcefield : MonoBehaviour
{
    //public string type = "FORCEFIELD";
    private bool isActive = true;
    private Sprite blankSprite;


    void Start()
    {
        blankSprite = Resources.Load<Sprite>("Sprites/BlankSprite");

        //check GameState to see if the boolean for this forcefield is false
        if (!(GameState.GetForcefieldActivatedStatus(SceneManager.GetActiveScene().name, this.name)))
        {
            Inactivate();
        }
    }

    void Update()
    {
        
    }

    public bool getIsActive ()
    {
        return isActive;
    }

    public void Inactivate()
    {
        isActive = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = blankSprite;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
