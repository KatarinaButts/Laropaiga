using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Forcefield : MonoBehaviour
{
    private bool isActive = true;
    private Sprite blankSprite;

    void Start()
    {
        blankSprite = Resources.Load<Sprite>("Sprites/BlankSprite");

        //Check GameState forcefield booleans to see if this forcefield has been inactivated
        if (!(GameState.GetForcefieldActivatedStatus(SceneManager.GetActiveScene().name, this.name)))
        {
            Inactivate();
        }
    }

    public bool GetIsActive ()
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
