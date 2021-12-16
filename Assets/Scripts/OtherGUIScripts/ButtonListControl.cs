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
    //[SerializeField]
    //private Button nextButton;

    [SerializeField]
    private WordButtonGroupController wordButtonGroupController;
    [SerializeField]
    private Transform assignButtonsPanel;
    [SerializeField]
    private GameObject[] wordButtons = new GameObject[6];

    void Start()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {

    }

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

        //wordButtonsPanel.gameObject.SetActive(false);

        if (learned == true)
        {
            Debug.Log("Clicked Button: " + wordJapanese);
            Debug.Log(wordJapanese + " learned. Displaying.");
            wordJapaneseText.text = wordJapanese;
            wordEnglishText.text = "English: " + wordEnglish;
            pronunciationText.text = "Pronunciation: " + pronunciation;
            partOfSpeechText.text = "Part of Speech: " + partOfSpeech;
            definitionText.text = "Definition: " + definition;

            //if(WordButtonGroupController.GetButtonsActivated() > 5)
            assignButton.gameObject.SetActive(true);
            useButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Clicked Button: " + wordJapanese);
            Debug.Log(wordJapanese + " not learned. Displaying alternate text.");

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

        Debug.Log("In AssignClickedTest() function");
        int wordButtonsAmount = wordButtonGroupController.getButtonsActive();
        Debug.Log("wordButtonsAmount = " + wordButtonsAmount);

        //Might need to fix this. Changed from List to an array
        GameObject[] buttonsfromGroupController = wordButtonGroupController.getWordButtonsArray();

        for (int i = 0; i < wordButtonsAmount; i++)
        {
            Debug.Log("i = " + i);
            wordButtons[i].SetActive(true);
            wordButtons[i].GetComponent<AssignWordButton>().SetEntry(buttonsfromGroupController[i].GetComponent<WordButton>().getDictionaryEntry());
            wordButtons[i].GetComponent<AssignWordButton>().SetChangeToEntry(currDictEntry);

            //wordTextSections[i].text = buttonsfromGroupController[i].GetComponent<WordButton>().getDictionaryEntry().GetJapaneseWord();
        }
        GameObject.FindObjectOfType<GameManager>().SetDisplayingAssignWordButtonPanel(true);

        assignButtonsPanel.gameObject.SetActive(true);

        //set the newly constructed word panel as inactive until this gets selected again

        //wordButtonsPanel.gameObject.SetActive(false);
    }


    /*
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }
    */
    /*
    IEnumerator ButtonWaitTest()
    {
        Debug.Log("In the IEnumerator ButtonWaitTest() function");

        var waitForButton = new WaitForUIButtons(wordButtons);
        yield return waitForButton.Reset();
        if(waitForButton.PressedButton == wordButton0)
        {
            Debug.Log("Clicked wordButton0");
            wordButton0.GetComponent<WordButton>().OnAssignFromDictionary(currDictEntry);
        }
        else// if(waitForButton.PressedButton == wordButton1)
        {
            Debug.Log("Clicked wordButton1");
            wordButton1.GetComponent<WordButton>().OnAssignFromDictionary(currDictEntry);
        }
        /*
        else if (waitForButton.PressedButton == wordButton2)
        {
            Debug.Log("Clicked wordButton2");
            wordButton2.GetComponent<WordButton>().OnAssignFromDictionary(currDictEntry);
        }
        else if (waitForButton.PressedButton == wordButton3)
        {
            Debug.Log("Clicked wordButton3");
            wordButton3.GetComponent<WordButton>().OnAssignFromDictionary(currDictEntry);
        }
        else if (waitForButton.PressedButton == wordButton4)
        {
            Debug.Log("Clicked wordButton4");
            wordButton4.GetComponent<WordButton>().OnAssignFromDictionary(currDictEntry);
        }
        else //wordButton5 was pressed
        {
            Debug.Log("Clicked wordButton5");
            wordButton5.GetComponent<WordButton>().OnAssignFromDictionary(currDictEntry);
        }
        
    }
    */

    //public void AssignedButtonClickTest () {
    
        //OnAssignFromDictionary(assignedEntry);
    //}
}
