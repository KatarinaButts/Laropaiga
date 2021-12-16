using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignWordButton : MonoBehaviour
{
    [SerializeField]
    Text displayJapaneseWord;
    [SerializeField]
    GameObject wordButton;

    DictionaryEntry entry;
    DictionaryEntry changeToEntry;


    public void SetEntry(DictionaryEntry newEntry)
    {
        entry = newEntry;
        displayJapaneseWord.text = entry.GetJapaneseWord();
    }

    public void SetChangeToEntry(DictionaryEntry newEntry)
    {
        changeToEntry = newEntry;
    }

    public void OnClicked()
    {
        wordButton.GetComponent<WordButton>().OnAssignFromDictionary(changeToEntry);
    }

}
