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

    private List<DictionaryEntry> dictionaryEntries;
    private List<Dialogue> currDialogueList;
    private GameObject currentInteractionCharacter;
    private GameObject player;

    //FileInfo sourceFile = new FileInfo("DictionaryEntries.txt");

    private static bool gameManagerExists;

    bool gamePaused;
    bool gameOver;
    bool displayingDialogue;
    bool inBattle;
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

    //ToDo: Implement UI

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
   


    /*
    public Transform pauseUIPanel;
    public Transform inventoryUIPanel;
    */

    //Dictionary Parameters
    /*
    int dictionaryIndex;
    public Text wordJapaneseText;
    public Text wordEnglishText;
    public Text pronunciationText;
    public Text partOfSpeechText;
    public Text definitionText;
    */
    //public Text imageText;
    //public Image dictImage;


    void Start()
    {

        gamePaused = false;
        gameOver = false;
        displayingDialogue = false;
        inBattle = false;
        menuOpen = false;
        inventoryOpen = false;
        dictionaryOpen = false;
        openedFromMenu = false;
        menuCamera.enabled = false;
        mainCamera.enabled = true;

        dialogueDisplayInt = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        //Locks mouse cursor to the game window (but still allows it to move from the center) for dual-monitor use
        //Cursor.lockState = CursorLockMode.Confined;

        //ToDo: Implement UI
        //ToDo: Change dictionary open to open from inventory buttons once inventory is implemented
        /*
        //UI Initialization
        //makes sure the pause menu is off when the scene starts
        pauseUIPanel.gameObject.SetActive(false);
        gamePaused = false;

        //makes sure the inventory is closed when the scene starts
        inventoryUIPanel.gameObject.SetActive(false);
        inventoryOpen = false;
        */

        //makes sure the menu is closed when the scene starts
        menuRightUIPanel.gameObject.SetActive(false);
        menuCameraImage.gameObject.SetActive(false);

        //makes sure the dictionary is closed when the scene starts
        menuCanvas.gameObject.SetActive(false);
        dictionaryRightUIPanel.gameObject.SetActive(false);
        dictionaryLeftUIPanel.gameObject.SetActive(false);
        dictionaryOpen = false;

        //makes sure the dialogue panel is closed when the scene starts
        dialoguePanel.gameObject.SetActive(false);

        //Dictionary Initialization
        dictionary.InitializeDictionaryFromFile();
        dictionaryEntries = dictionary.GetDictionaryEntries();
        /*
        Debug.Log("about to look at the list in GameManager script");
        foreach (DictionaryEntry b in dictionaryEntries)
        {
            Debug.Log("GameManager's dictionaryEntries: " + b.GetJapaneseWord());
            Debug.Log("GameManager's dictionaryEntries: " + b.GetEnglishWord());
            Debug.Log("GameManager's dictionaryEntries: " + b.GetDefinition());
        }
        */

        dictionaryRightUIPanel.GetComponent<ButtonListControl>().initDictionaryList(dictionaryEntries);
        //dictionaryRightUIPanel.GetComponent<ButtonListControl>().GenerateButtons(dictionaryEntries);

        //makes sure the word selection is closed when the scene starts
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
    }

    public bool getDisplayingDialogue()
    {
        return displayingDialogue;
    }

    public void SetDisplayingAssignWordButtonPanel(bool displaying)
    {
        displayingAssignWordButtonPanel = displaying;
    }

    void DisplayDictionaryPageInfo()
    {
        /*
        wordJapaneseText.text = "Japanese: " + dictionaryEntries[dictionaryIndex].GetJapaneseWord();
        wordEnglishText.text = "English: " + dictionaryEntries[dictionaryIndex].GetEnglishWord();
        pronunciationText.text = "Pronunciation: " + dictionaryEntries[dictionaryIndex].GetPronunciation();
        partOfSpeechText.text = "Part of Speech: " + dictionaryEntries[dictionaryIndex].GetPartOfSpeech();
        definitionText.text = "Definition: " + dictionaryEntries[dictionaryIndex].GetDefinition();
        */
    }

    void KeyChecking()
    {
        //!!!Update below with another && statement whenever a new UI is added!!!

        /*
        //Pause and unpause game keys and checking
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            Debug.Log("ENTERED TRYING TO PAUSE");
            if (addressBookIsOpen == false && placingBuildingsMode == false && placingRoadsMode == false)
            {
                PauseGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            Debug.Log("ENTERED TRYING TO UNPAUSE");
            if (addressBookIsOpen == false && placingBuildingsMode == false && placingRoadsMode == false)
            {
                UnPauseGame();
            }
        }

        //Open and close inventory keys and checking
        if (Input.GetKeyDown(KeyCode.I) && inventoryIsOpen == false)
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.I) && inventoryIsOpen == true)
        {
            CloseInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && addressBookIsOpen == true)
        {
            CloseAddressBook();
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && placingBuildingsMode == true)
        {
            placingBuildingsMode = false;
            _gridOverlayScript.HideGrid();
            gridBlock.SetActive(false);
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && placingRoadsMode == true)
        {
            placingRoadsMode = false;
            _gridOverlayScript.HideGrid();
            roadPlacementBlock.SetActive(false);
            OpenInventory();
        }

        if (addressBookIsOpen == true && outOfBuildings == false && buildingObjectsList.Count > 1)
        {
            //Debug.Log("->->     <-<-\nShould be able to move back and forth now");
            //Debug.Log("addressBookIndex: " + addressBookIndex);
            //Debug.Log("buildingObjectsList.Count: " + buildingObjectsList.Count);

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (addressBookIndex <= 0)
                {
                    Debug.Log("You'll go out of bounds if you go this way");
                }
                else if (addressBookIndex > 0)
                {
                    //Debug.Log("You're going left in the list!");
                    addressBookIndex -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (addressBookIndex + 1 >= buildingObjectsList.Count)
                {
                    Debug.Log("You'll go out of bounds if you go this way");
                }
                else if (addressBookIndex + 1 < buildingObjectsList.Count)
                {
                    //Debug.Log("You're going right in the list!");
                    addressBookIndex += 1;
                }
            }

        }
        */

        if (Input.GetKeyDown(KeyCode.Space) && displayingDialogue == true || Input.GetKeyDown(KeyCode.KeypadEnter) && displayingDialogue == true || Input.GetKeyDown(KeyCode.Return) && displayingDialogue == true)
        {
            //Debug.Log("ToDo: Implement Fighting and win bool so we can see if they gain money and friendship point");
            if (dialogueDisplayInt + 1 < currDialogueList.Count)
            {
                //player.GetComponent<PlayerController>().setAllowMovement(false);
                //Debug.Log("***Displaying Next Dialogue***");
                //display next
                dialogueDisplayInt += 1;
                ManageDialogue(currDialogueList, currentInteractionCharacter);
            }
            else
            {
                //stop displaying
                //Debug.Log("***Stopping Dialogue***");
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

        //!!!temp item removal checking
        if (Input.GetKeyDown(KeyCode.U) && inventoryOpen == true)
        {
            this.GetComponent<Inventory>().addItem(this.GetComponent<Inventory>().findItem("TestEnglishName"));
        }


        //!!!temp battle check
        if (Input.GetKeyDown(KeyCode.B) && inBattle == false)
        {
            //start battle
            battleCamera.enabled = true;
            mainCamera.enabled = false;
            player.GetComponent<PlayerController>().setAllowMovement(false);
            inBattle = true;
        }
        else if (Input.GetKeyDown(KeyCode.B) && inBattle == true)
        {
            //end battle
            mainCamera.enabled = true;
            battleCamera.enabled = false;
            player.GetComponent<PlayerController>().setAllowMovement(true);
            inBattle = false;
        }
        

        //Assign Word Panel closer
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

        //Menu Opener
        else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen == false)
        {
            OpenMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuOpen == true)
        {
            CloseMenu();
        }
        /*
        //Temp dictionary opener
        //Open and close dictionary keys and checking
        if (Input.GetKeyDown(KeyCode.I) && dictionaryOpen == false)
        {
            OpenDictionary();
        }
        else if (Input.GetKeyDown(KeyCode.I) && dictionaryOpen == true)
        {
            CloseDictionary();
        }
        */
    }

    /*
    public void OpenInventory()
    {
        Debug.Log("Opened Inventory");
        inventoryIsOpen = true;
        inventoryUIPanel.gameObject.SetActive(true);
    }
    */

    public void OpenMenu()
    {
        player.GetComponent<PlayerController>().setAllowMovement(false);
        //Debug.Log("Opened Menu");
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

        //Debug.Log("Closed Menu");
        if(dictionaryOpen == false && inventoryOpen == false  /*&&*/)    //no other submenu is in use
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
        Debug.Log("Opened Dictionary");
        Debug.Log("Generating Buttons for Dictionary");
        dictionaryRightUIPanel.GetComponent<ButtonListControl>().GenerateButtons(dictionaryEntries);

        menuCanvas.gameObject.SetActive(true);
        //if (menuPanel.gameObject.activeSelf == true)
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
        Debug.Log("Closed Dictionary");
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
        Debug.Log("Opened Inventory");
        menuCanvas.gameObject.SetActive(true);
        //if (menuPanel.gameObject.activeSelf == true)
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
        Debug.Log("Closed Inventory");
        //menuCanvas.gameObject.SetActive(false);
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
        //Debug.Log("***ManagingDialogue***");
        player.GetComponent<PlayerController>().setAllowMovement(false);

        dialoguePanel.gameObject.SetActive(true);
        currentInteractionCharacter = currInteraction;
        displayingDialogue = true;
        currDialogueList = dialogueList;
        Dialogue currDialogue = currDialogueList[dialogueDisplayInt];

        //Debug.Log("DisplayingCurrentDialogueSection: " + currDialogue.getDialogueSection());
        dialogueTextField.text = currDialogue.getDialogueSection();
        dialogueCharacterNameField.text = currInteraction.name;
        dialogueCharacterSprite.sprite = currInteraction.GetComponent<SpriteRenderer>().sprite;

        if (currDialogue.getFightBool())
        {
            //Debug.Log("ToDo: Implement Fighting and win bool so we can see if they gain money and friendship point");
            //if fight won
            //Debug.Log("Displaying WinDialogue: " + currDialogue.getWinDialogue());
            //else if fight lost
            //Debug.Log("Displaying LoseDialogue: " + currDialogue.getLoseDialogue());
        }
        if (currDialogue.getWordLearned() != null && currDialogue.getWordLearned() != "")
        {
            //Debug.Log("!!!Current: Find word in Dictionary List & DictionaryManager and change the bool learned state to true!!!");
            int dictWordIndex = FindDictionaryWord(currDialogue.getWordLearned());
           if(dictWordIndex > -1)
            {
                dictionaryEntries[dictWordIndex].LearnWord();
                dictionary.GetDictionaryEntries()[dictWordIndex].LearnWord();

                //Debug.Log("dictionaryEntries[dictWordIndex].GetJapaneseWord()" + dictionaryEntries[dictWordIndex].GetJapaneseWord());
                //Debug.Log("dictionary.GetDictionaryEntries()[dictWordIndex].GetJapaneseWord()" + dictionaryEntries[dictWordIndex].GetJapaneseWord());

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
                Debug.Log("Could not find " + currDialogue.getWordLearned() + " in the dictionary");
            }
        }
       
    }

    public int FindDictionaryWord(string searchWord)
    {
        Debug.Log("FindDictionaryWord(" + searchWord  + ")");

        int i = dictionaryEntries.FindIndex(a => a.GetSearchWord().ToLower() == searchWord.ToLower());
        Debug.Log("FindDictionaryWord index: " + i);
        if(i > -1)
        {
            Debug.Log("dictionaryEntries[i].GetJapaneseWord()" + dictionaryEntries[i].GetJapaneseWord());
        }

        return i;
    }
}
