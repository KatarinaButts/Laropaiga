using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordButton : MonoBehaviour
{
    [SerializeField]
    Text displayJapaneseWord;
    [SerializeField]
    Sprite activeImage;
    [SerializeField]
    Sprite inactiveImage;
    [SerializeField]
    private WordButtonGroupController wordButtonGroupController;

    DictionaryEntry entry;
    public bool activated = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Setters
    //Initial setup of entry
    public void SetEntry(DictionaryEntry newEntry)
    {
        entry = newEntry;
        displayJapaneseWord.text = entry.GetJapaneseWord();
    }

    public DictionaryEntry getDictionaryEntry()
    {
        return entry;
    }

    public bool getActiveState()
    {
        return activated;
    }

    public void changeActiveState()
    {
        activated = !activated;
        if (activated)
        {
            GetComponent<Image>().sprite = activeImage;
        }
        else
        {
            GetComponent<Image>().sprite = inactiveImage;
        }
    }

    public void OnClick()
    {
        Debug.Log("Entered WordButton OnClick() method");
        if (activated == false)  //change to active on click
        {
            Debug.Log("activated == " + activated);
            activated = true;
            GetComponent<Image>().sprite = activeImage;
            
        }
        else     //change to inactive on click
        {
            Debug.Log("activated == " + activated);
            activated = false;
            GetComponent<Image>().sprite = inactiveImage;
        }

        Debug.Log("activated at end of OnClick function == " + activated);
        Debug.Log("this.getActiveState() == " + this.getActiveState());

        wordButtonGroupController.ButtonClicked(this);
    }

    public void OnAssignFromDictionary(DictionaryEntry assignedEntry)
    {
        wordButtonGroupController.UpdateButton(this, assignedEntry);
    }
}
