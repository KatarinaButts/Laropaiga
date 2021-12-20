using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private List<DictionaryEntry> dictEntries;
    private List<GameObject> buttons = new List<GameObject>();
    private DictionaryEntry currDictEntry;

    public Text wordJapaneseText;
    public Text wordEnglishText;
    public Text pronunciationText;
    public Text partOfSpeechText;
    public Text definitionText;

    [SerializeField]
    private GameObject assignButton;
    [SerializeField]
    private GameObject useButton;

    [SerializeField]
    private WordButtonGroupController wordButtonGroupController;
    [SerializeField]
    private Transform assignButtonsPanel;
    [SerializeField]
    private GameObject[] wordButtons = new GameObject[6];

    public void initDictionaryList(List<DictionaryEntry> dictionaryEntries)
    {
        dictEntries = dictionaryEntries;
    }

    public void GenerateButtons(List<DictionaryEntry> dictionaryEntries)
    {
        useButton.SetActive(false);
        assignButton.SetActive(false);

        if (buttons != null && buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        dictEntries = dictionaryEntries;

        for (int i = 0; i < dictEntries.Count; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            if (dictEntries[i].GetLearned())
            {
                button.GetComponent<ButtonListButton>().SetText(dictEntries[i].GetJapaneseWord());
            }
            else
            {
                button.GetComponent<ButtonListButton>().SetText("???");
            }
            button.GetComponent<ButtonListButton>().SetData(dictEntries[i]);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button.gameObject);
        }
    }
    public void ButtonClicked(DictionaryEntry dictionaryEntry)
    {
        currDictEntry = dictionaryEntry;
        bool learned = dictionaryEntry.GetLearned();

        string wordJapanese = dictionaryEntry.GetJapaneseWord();
        string wordEnglish = dictionaryEntry.GetEnglishWord();
        string pronunciation = dictionaryEntry.GetPronunciation();
        string partOfSpeech = dictionaryEntry.GetPartOfSpeech();
        string definition = dictionaryEntry.GetDefinition();

        if (learned == true)
        {
            wordJapaneseText.text = wordJapanese;
            wordEnglishText.text = "English: " + wordEnglish;
            pronunciationText.text = "Pronunciation: " + pronunciation;
            partOfSpeechText.text = "Part of Speech: " + partOfSpeech;
            definitionText.text = "Definition: " + definition;

            assignButton.gameObject.SetActive(true);
            useButton.gameObject.SetActive(true);
        }
        else
        {
            wordJapaneseText.text = "???";
            wordEnglishText.text = "English: " + "???";
            pronunciationText.text = "Pronunciation: " + "???";
            partOfSpeechText.text = "Part of Speech: " + "???";
            definitionText.text = "Definition: " + "???";

            assignButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
    }

    public void AssignClickedTest()
    {
        foreach (GameObject a in wordButtons)
        {
            a.SetActive(false);
        }
        int wordButtonsAmount = wordButtonGroupController.getButtonsActive();

        GameObject[] buttonsfromGroupController = wordButtonGroupController.getWordButtonsArray();

        for (int i = 0; i < wordButtonsAmount; i++)
        {
            Debug.Log("i = " + i);
            wordButtons[i].SetActive(true);
            wordButtons[i].GetComponent<AssignWordButton>().SetEntry(buttonsfromGroupController[i].GetComponent<WordButton>().getDictionaryEntry());
            wordButtons[i].GetComponent<AssignWordButton>().SetChangeToEntry(currDictEntry);

        }
        GameObject.FindObjectOfType<GameManager>().SetDisplayingAssignWordButtonPanel(true);

        assignButtonsPanel.gameObject.SetActive(true);
    }
}
