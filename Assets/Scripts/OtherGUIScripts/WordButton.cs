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
        if (activated == false)  //change to active on click
        {
            activated = true;
            GetComponent<Image>().sprite = activeImage;
            
        }
        else     //change to inactive on click
        {
            activated = false;
            GetComponent<Image>().sprite = inactiveImage;
        }

        wordButtonGroupController.ButtonClicked(this);
    }

    public void OnAssignFromDictionary(DictionaryEntry assignedEntry)
    {
        wordButtonGroupController.UpdateButton(this, assignedEntry);
    }
}
