using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DictionaryManager dictionary;
    [SerializeField]
    private WordButtonGroupController wordButtonGroupController;
    [SerializeField]
    private BattleController battleController;

    private List<DictionaryEntry> dictionaryEntries;
    private List<Dialogue> currDialogueList;
    private GameObject currentInteractionCharacter;
    private GameObject player;

    private static bool gameManagerExists;

    bool gameOver;
    bool displayingDialogue;
    bool inBattle;
    bool battleWon;
    bool menuOpen;
    bool inventoryOpen;
    bool dictionaryOpen;
    bool openedFromMenu;
    bool displayingAssignWordButtonPanel;
    public Camera mainCamera;
    [SerializeField]
    Camera battleCamera;

    int dialogueDisplayInt;
    int showDictWordButtons;

    //UI Panels
    public Transform menuCanvas;
    public Transform dictionaryRightUIPanel;
    public Transform dictionaryLeftUIPanel;

    public Transform inventoryMainPanel;
    public Transform inventoryRightPanel;
    public Transform inventoryLeftPanel;

    public Transform menuPanel;
    public Transform menuRightUIPanel;
    public RawImage menuCameraImage;
    public Camera menuCamera;

    [SerializeField]
    private Transform dialoguePanel;
    [SerializeField]
    private Text dialogueTextField;
    [SerializeField]
    private Text dialogueCharacterNameField;
    [SerializeField]
    private Image dialogueCharacterSprite;

    [SerializeField]
    private Transform wordSelectionPanel;

    void Start()
    {
        gameOver = false;
        displayingDialogue = false;
        inBattle = false;
        battleWon = false;
        menuOpen = false;
        inventoryOpen = false;
        dictionaryOpen = false;
        openedFromMenu = false;
        menuCamera.enabled = false;
        mainCamera.enabled = true;

        dialogueDisplayInt = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        //Locks mouse cursor to the game window (but still allows it to move from the center) for dual-monitor use
        Cursor.lockState = CursorLockMode.Confined;

        //Makes sure the menu is closed when the scene starts
        menuRightUIPanel.gameObject.SetActive(false);
        menuCameraImage.gameObject.SetActive(false);

        //Makes sure the dictionary is closed when the scene starts
        menuCanvas.gameObject.SetActive(false);
        dictionaryRightUIPanel.gameObject.SetActive(false);
        dictionaryLeftUIPanel.gameObject.SetActive(false);
        dictionaryOpen = false;

        //Makes sure the dialogue panel is closed when the scene starts
        dialoguePanel.gameObject.SetActive(false);

        //Dictionary Initialization
        dictionary.InitializeDictionaryFromFile();
        dictionaryEntries = dictionary.GetDictionaryEntries();

        dictionaryRightUIPanel.GetComponent<ButtonListControl>().initDictionaryList(dictionaryEntries);

        //Makes sure the word selection section is closed when the scene starts
        wordSelectionPanel.gameObject.SetActive(false);

        if (!gameManagerExists)
        {
            gameManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        KeyChecking();

        //Battle check test
        if (inBattle == false && GameState.battleScene == true && (player.GetComponent<PlayerController>().getSteps >= 25.0f))
        {
            inBattle = true;
            player.GetComponent<PlayerController>().setAllowMovement(false);
            battleController.StartBattle();
            //Start battle
            battleCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }

    public bool GetDisplayingDialogue()
    {
        return displayingDialogue;
    }

    public void SetDisplayingAssignWordButtonPanel(bool displaying)
    {
        displayingAssignWordButtonPanel = displaying;
    }

    /*
    void DisplayDictionaryPageInfo()
    {
        
        wordJapaneseText.text = "Japanese: " + dictionaryEntries[dictionaryIndex].GetJapaneseWord();
        wordEnglishText.text = "English: " + dictionaryEntries[dictionaryIndex].GetEnglishWord();
        pronunciationText.text = "Pronunciation: " + dictionaryEntries[dictionaryIndex].GetPronunciation();
        partOfSpeechText.text = "Part of Speech: " + dictionaryEntries[dictionaryIndex].GetPartOfSpeech();
        definitionText.text = "Definition: " + dictionaryEntries[dictionaryIndex].GetDefinition();
        

    }
    */

        void KeyChecking()
    {

        if (Input.GetKeyDown(KeyCode.Space) && displayingDialogue == true || Input.GetKeyDown(KeyCode.KeypadEnter) && displayingDialogue == true || Input.GetKeyDown(KeyCode.Return) && displayingDialogue == true)
        {
            //display next dialogue section
            if (dialogueDisplayInt + 1 < currDialogueList.Count)
            {
                dialogueDisplayInt += 1;
                ManageDialogue(currDialogueList, currentInteractionCharacter);
            }
            //stop displaying
            else { 
                    displayingDialogue = false;
                dialogueDisplayInt = 0;
                if(currentInteractionCharacter.GetComponent<NonPlayerCharacter>())
                {
                    currentInteractionCharacter.GetComponent<NonPlayerCharacter>().setFriendshipPoints(1);
                }
                player.GetComponent<PlayerController>().setAllowMovement(true);
                dialoguePanel.gameObject.SetActive(false);
            }
        }

        //Temporary inventory item removal checking
        /*
        if (Input.GetKeyDown(KeyCode.U) && inventoryOpen == true)
        {
            this.GetComponent<Inventory>().addItem(this.GetComponent<Inventory>().findItem("TestEnglishName"));
        }
        */

        //Assign word panel closer
        if (Input.GetKeyDown(KeyCode.Escape) && displayingAssignWordButtonPanel == true)
        {
            GameObject assignWordButtonMainPanel = GameObject.Find("AssignWordButtonMainPanel");
            assignWordButtonMainPanel.SetActive(false);
            displayingAssignWordButtonPanel = false;
        }
        //Dictionary closer
        else if (Input.GetKeyDown(KeyCode.Escape) && dictionaryOpen == true)
        {
            CloseDictionary();
        }
        //Inventory closer
        else if(Input.GetKeyDown(KeyCode.Escape) && inventoryOpen == true)
        {
            CloseInventory();
        }

        //Menu opener
        else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen == false)
        {
            OpenMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuOpen == true)
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        player.GetComponent<PlayerController>().setAllowMovement(false);
        menuCanvas.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(true);
        menuRightUIPanel.gameObject.SetActive(true);
        menuCameraImage.gameObject.SetActive(true);
        menuCamera.enabled = true;
        mainCamera.enabled = false;

        menuOpen = true;
    }

    public void CloseMenu()
    {
        if(dictionaryOpen == false && inventoryOpen == false)    //If no submenu is in use
        {
            player.GetComponent<PlayerController>().setAllowMovement(true);
            menuCanvas.gameObject.SetActive(false);
        }
        menuPanel.gameObject.SetActive(false);
        menuRightUIPanel.gameObject.SetActive(false);
        menuCameraImage.gameObject.SetActive(false);
        mainCamera.enabled = true;
        menuCamera.enabled = false;

        menuOpen = false;
    }

    public void OpenDictionary()
    {
        player.GetComponent<PlayerController>().setAllowMovement(false);

        //generating buttons for dictionary
        dictionaryRightUIPanel.GetComponent<ButtonListControl>().GenerateButtons(dictionaryEntries);

        menuCanvas.gameObject.SetActive(true);
        dictionaryRightUIPanel.gameObject.SetActive(true);

        dictionaryLeftUIPanel.gameObject.SetActive(true);
        dictionaryOpen = true;

        if (menuOpen == true)
        {
            openedFromMenu = true;
            CloseMenu();
        }

    }

    public void CloseDictionary()
    {
        dictionaryRightUIPanel.gameObject.SetActive(false);
        dictionaryLeftUIPanel.gameObject.SetActive(false);
        dictionaryOpen = false;
        if(openedFromMenu == true)
        {
            player.GetComponent<PlayerController>().setAllowMovement(false);
            OpenMenu();
            openedFromMenu = false;
        }
        else
        {
            player.GetComponent<PlayerController>().setAllowMovement(true);
            menuCanvas.gameObject.SetActive(false);
        }
    }

    public void OpenInventory()
    {
        menuCanvas.gameObject.SetActive(true);
        inventoryMainPanel.gameObject.SetActive(true);
        inventoryRightPanel.gameObject.SetActive(true);
        inventoryLeftPanel.gameObject.SetActive(true);

        inventoryOpen = true;

        if (menuOpen == true)
        {
            openedFromMenu = true;
            CloseMenu();
        }
        player.GetComponent<PlayerController>().setAllowMovement(false);

    }
    public void CloseInventory()
    {
        inventoryMainPanel.gameObject.SetActive(false);
        inventoryRightPanel.gameObject.SetActive(false);
        inventoryLeftPanel.gameObject.SetActive(false);

        inventoryOpen = false;
        if(openedFromMenu == true)
        {
            player.GetComponent<PlayerController>().setAllowMovement(false);
            OpenMenu();
            openedFromMenu = false;
        }
        else
        {
            player.GetComponent<PlayerController>().setAllowMovement(true);
            menuCanvas.gameObject.SetActive(false);
        }
    }

    public void ManageDialogue(List<Dialogue> dialogueList, GameObject currInteraction)
    {
        player.GetComponent<PlayerController>().setAllowMovement(false);

        dialoguePanel.gameObject.SetActive(true);
        currentInteractionCharacter = currInteraction;
        displayingDialogue = true;
        currDialogueList = dialogueList;
        Dialogue currDialogue = currDialogueList[dialogueDisplayInt];

        dialogueTextField.text = currDialogue.GetDialogueSection();
        dialogueCharacterNameField.text = currInteraction.name;
        dialogueCharacterSprite.sprite = currInteraction.GetComponent<SpriteRenderer>().sprite;

        if (currDialogue.GetFightBool())
        {

        }
        if (currDialogue.GetWordLearned() != null && currDialogue.GetWordLearned() != "")
        {
           int dictWordIndex = FindDictionaryWord(currDialogue.GetWordLearned());
            //If the word was found
           if(dictWordIndex > -1)
            {
                dictionaryEntries[dictWordIndex].LearnWord();
                dictionary.GetDictionaryEntries()[dictWordIndex].LearnWord();

                showDictWordButtons += 1;
                switch(showDictWordButtons)
                {
                    case 1:
                        wordSelectionPanel.gameObject.SetActive(true);
                        wordButtonGroupController.activateButton(0, dictionaryEntries[dictWordIndex]);
                        break;
                    case 2:
                        wordButtonGroupController.activateButton(1, dictionaryEntries[dictWordIndex]);
                        break;
                    case 3:
                        wordButtonGroupController.activateButton(2, dictionaryEntries[dictWordIndex]);
                        break;
                    case 4:
                        wordButtonGroupController.activateButton(3, dictionaryEntries[dictWordIndex]);
                        break;
                    case 5:
                        wordButtonGroupController.activateButton(4, dictionaryEntries[dictWordIndex]);
                        break;
                    case 6:
                        wordButtonGroupController.activateButton(5, dictionaryEntries[dictWordIndex]);
                        break;
                    default:
                        break;
                }
            }
           else
            {
                Debug.Log("Could not find " + currDialogue.GetWordLearned() + " in the dictionary");
            }
        }
       
    }

    public int FindDictionaryWord(string searchWord)
    {
        int i = -1;
        i = dictionaryEntries.FindIndex(a => a.GetSearchWord().ToLower() == searchWord.ToLower());

        return i;
    }

    //Initialized from BattleController once battle has ended
    public void EndBattle(bool battleWon)
    {
        //end battle
        mainCamera.enabled = true;
        battleCamera.enabled = false;
        player.GetComponent<PlayerController>().ResetSteps();
        player.GetComponent<PlayerController>().setAllowMovement(true);
        inBattle = false;
    }
}
