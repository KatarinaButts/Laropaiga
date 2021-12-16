using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private ButtonListControl buttonControl;

    private DictionaryEntry dictEntry;
    private string wordJapanese;
    private string wordEnglish;
    private string pronunciation;
    private string partOfSpeech;
    private string definition;
    private string myTextString;
    bool learned;

    public void SetText(string textString)
    {
        myTextString = textString;
        myText.text = textString;
    }
    public void SetData(DictionaryEntry dictionaryEntry)
    {
        dictEntry = dictionaryEntry;
        wordJapanese = dictionaryEntry.GetJapaneseWord();
        wordEnglish = dictionaryEntry.GetEnglishWord();
        pronunciation = dictionaryEntry.GetPronunciation();
        partOfSpeech = dictionaryEntry.GetPartOfSpeech();
        definition = dictionaryEntry.GetDefinition();
        learned = dictionaryEntry.GetLearned();
    }

    public void OnClick()
    {
        buttonControl.ButtonClicked(dictEntry);

    }

}
