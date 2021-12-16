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

    //private List<DictionaryEntry> entries;
   

    private void Awake()
    {
      
    }

    void Start()
    {
        foreach (GameObject a in wordButtons)
        {
            a.SetActive(false);
        }

        buttonsActivated = 0;
    }

    void Update()
    {
        //Empty
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
        Debug.Log("***Activating Button***");
        wordButtons[i].GetComponent<WordButton>().SetEntry(entry);
        //words[i] = wordButtons[i].GetComponent<WordButton>();
        //words[i].SetEntry(entry);
        Debug.Log("word JapaneseWord: " + wordButtons[i].GetComponent<WordButton>().getDictionaryEntry().GetJapaneseWord());
        wordButtons[i].SetActive(true);
        buttonsActivated += 1;
        //if(i == 0)  //first one activated
        //{
        //    wordButtons[i].GetComponent<WordButton>().changeActiveState();
        //}
    }

    public void UpdateButton(WordButton button, DictionaryEntry entry) 
    {
        int buttonClickedIndex = -1; //didn't find

        for (int i = 0; i < wordButtons.Length; i++)
        {
            if (wordButtons[i].name == button.name)
            {
                buttonClickedIndex = i;
                break;
            }
        }
        Debug.Log("buttonClickedIndex = " + buttonClickedIndex);
        bool activeState = wordButtons[buttonClickedIndex].GetComponent<WordButton>().getActiveState();
        Debug.Log("activeState = " + activeState);

        wordButtons[buttonClickedIndex].GetComponent<WordButton>().SetEntry(entry);

        GameObject.FindObjectOfType<GameManager>().SetDisplayingAssignWordButtonPanel(false);

        assignWordPanel.gameObject.SetActive(false);
    }

    public void ButtonClicked(WordButton button)
    {
        int buttonClickedIndex = -1; //didn't find

        //find button in array
        for(int i = 0; i < wordButtons.Length; i++)
        {
            if (wordButtons[i].name == button.name)
            {
                buttonClickedIndex = i;
                break;
            }
        }

        //int buttonClickedIndex = wordButtons.FindIndex(a => a.GetComponent<WordButton>().name == button.name);
        //Debug.Log("***Found Word in WordButtonGroupController ButtonClicked()***");
        //Debug.Log("wordButtons[0].GetComponent<WordButton>().name = " + wordButtons[1].GetComponent<WordButton>().name);


        //Debug.Log("buttonClickedIndex = " + buttonClickedIndex);
        bool activeState = wordButtons[buttonClickedIndex].GetComponent<WordButton>().getActiveState();
        Debug.Log("activeState = " + wordButtons[buttonClickedIndex].GetComponent<WordButton>().getActiveState());


        Debug.Log("wordButtons[buttonClickedIndex] Japanese Word: " + wordButtons[buttonClickedIndex].GetComponent<WordButton>().getDictionaryEntry().GetJapaneseWord());

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
            Debug.Log("Button has been turned off");
        }

    }
}
