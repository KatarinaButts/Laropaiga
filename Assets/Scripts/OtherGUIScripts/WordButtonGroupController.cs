using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordButtonGroupController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wordButtons = new GameObject[6];
    [SerializeField]
    private Transform assignWordPanel;
    [SerializeField]
    private DictionaryManager dictionaryManager;
    
    int buttonsActivated;   

    void Start()
    {
        foreach (GameObject a in wordButtons)
        {
            a.SetActive(false);
        }

        buttonsActivated = 0;
    }

    public int getButtonsActive()
    {
        return buttonsActivated;
    }

    public GameObject[] getWordButtonsArray()
    {
        return wordButtons;
    }

    public void activateButton(int i, DictionaryEntry entry)
    {
        wordButtons[i].GetComponent<WordButton>().SetEntry(entry);
      
        wordButtons[i].SetActive(true);
        buttonsActivated += 1;
    }

    public void UpdateButton(WordButton button, DictionaryEntry entry) 
    {
        int buttonClickedIndex = -1;    //If it stays at -1, then we did not find the button

        for (int i = 0; i < wordButtons.Length; i++)
        {
            if (wordButtons[i].name == button.name)
            {
                buttonClickedIndex = i;
                break;
            }
        }
        bool activeState = wordButtons[buttonClickedIndex].GetComponent<WordButton>().getActiveState();

        wordButtons[buttonClickedIndex].GetComponent<WordButton>().SetEntry(entry);

        GameObject.FindObjectOfType<GameManager>().SetDisplayingAssignWordButtonPanel(false);

        assignWordPanel.gameObject.SetActive(false);
    }

    public void ButtonClicked(WordButton button)
    {
        int buttonClickedIndex = -1; //If it stays at -1, then we did not find the button

        //find button in array
        for (int i = 0; i < wordButtons.Length; i++)
        {
            if (wordButtons[i].name == button.name)
            {
                buttonClickedIndex = i;
                break;
            }
        }

        bool activeState = wordButtons[buttonClickedIndex].GetComponent<WordButton>().getActiveState();

        if (activeState == true)
        {
            for (int i = 0; i < wordButtons.Length; i++)
            {
                if(i == buttonClickedIndex)
                {
                }
                else
                {
                    if (wordButtons[i].GetComponent<WordButton>().getActiveState() == true)
                    {
                        wordButtons[i].GetComponent<WordButton>().changeActiveState();
                    }
                }
            }
            dictionaryManager.SetActiveWord(wordButtons[buttonClickedIndex].GetComponent<WordButton>().getDictionaryEntry());
        }
        else if (activeState == false)
        {
            //Debug.Log("Button has been turned off");
        }

    }
}
