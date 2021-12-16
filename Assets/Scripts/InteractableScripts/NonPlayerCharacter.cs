using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : Interactable
{

    //string characterName = "";  //ToDo: Add UI character name text section to DialogueController UI element
    //public float displayTime = 4.0f;
    //public Transform dialogBox;
    //float timerDisplay;
    int friendshipPoints;

    [SerializeField]
    string xmlTextFileName;

    XmlDocument dialogueDataXml;

    private List<Dialogue> dialogueList = new List<Dialogue>();
    private List<Dialogue> currDialogueList = new List<Dialogue>();
    private GameManager gameManager;
    int currDialogueListPos;

    //public string type = "NPC";

    //bool displayingDialogue = false;
    //int i = 0;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        //dialogBox.gameObject.SetActive(false);
        //timerDisplay = -1.0f;
        friendshipPoints = 0;
        currDialogueListPos = 0;

        TextAsset xmlTextAsset = Resources.Load<TextAsset>("TextFiles/" + xmlTextFileName);

        CreateDialogueList(xmlTextAsset);
        //Debug.Log("dialogueList.Count: " + dialogueList.Count);

        //Test
        foreach (Dialogue dialogueObject in dialogueList)
        {
            //Debug.Log(dialogueObject.getDialogueSection());
            //Debug.Log("FriendshipPointsReq: " + dialogueObject.getFriendshipPointsReq());
            //Debug.Log("Word Learned: " + dialogueObject.getWordLearned());
            //Debug.Log("Fight: " + dialogueObject.getFightBool());
            //Debug.Log("Win Dialogue: " + dialogueObject.getWinDialogue());
            //Debug.Log("Lose Dialogue: " + dialogueObject.getLoseDialogue());
        }
        CreateCurrentDialogueList();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0)
            {
                dialogBox.gameObject.SetActive(false);
            }
        }
        */
    }

    public List<Dialogue> getCurrDialogueList()
    {
        return currDialogueList;
    }

    public int getFriendshipPoints()
    {
        return friendshipPoints;
    }

    public void setFriendshipPoints(int addedFriendshipPoints)
    {
        friendshipPoints += addedFriendshipPoints;
        Debug.Log("Setting Friendship Points. New Friendship Points: " + friendshipPoints);
        //clears the list to prepare for a new list
        GameStateChangeCheck();

        currDialogueList.Clear();
        CreateCurrentDialogueList();
    }

    public void DisplayDialog()
    {
        gameManager.ManageDialogue(currDialogueList, this.gameObject);
        //timerDisplay = displayTime;
        //dialogBox.gameObject.SetActive(true);
    }

    public override void Interact(Transform PlayerTransform)
    {
        //DictionaryManager dictManager = GameObject.FindObjectOfType<GameManager>().GetComponent<DictionaryManager>();
        DictionaryManager dictManager = gameManager.GetComponent<DictionaryManager>();
        string searchWord = "hanasu";

        //Debug.Log("dictManager.GetActiveWord().GetSearchWord().ToLower() = " + dictManager.GetActiveWord().GetSearchWord().ToLower());
        //Debug.Log("active word: " + dictManager.GetActiveWord());
        //Debug.Log("search word: " + dictManager.GetActiveWord().GetSearchWord());

        //Debug.Log("dictManager.GetActiveWord() != null: " + (dictManager.GetActiveWord() != null));
        //Debug.Log("dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord) = " + dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord));


        if((SceneManager.GetActiveScene().name.CompareTo("MainScene") == 0) && this.gameObject.name == "Mouse" && friendshipPoints == 0)
        {
            //Debug.Log("This object is CatSitFront");

            base.Interact(PlayerTransform);
            //Debug.Log("Interacting with " + transform.name);

            if (!gameManager.getDisplayingDialogue())
            {
                DisplayDialog();
            }
        }

        else if (dictManager.GetActiveWord() != null && dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord))
        {
            base.Interact(PlayerTransform);
            //Debug.Log("Interacting with " + transform.name);

            if (!gameManager.getDisplayingDialogue())
            {
                DisplayDialog();
            }
        }
        else
        {
            //bring up a description/tooltip box
            //Debug.Log("You don't know this word or it's not currently your active word");
        }

    }

    void CreateDialogueList(TextAsset xmlTextAsset)
    {
        dialogueDataXml = new XmlDocument();
        dialogueDataXml.LoadXml(xmlTextAsset.text);
        XmlNodeList dialogueSections = dialogueDataXml.SelectNodes("/Dialogue/DialogueSection");
        foreach (XmlNode dialogueSection in dialogueSections)
        {
            Dialogue characterDialogueItem = new Dialogue();
            characterDialogueItem.setData(dialogueSection);
            dialogueList.Add(characterDialogueItem);
        }
    }

    void CreateCurrentDialogueList()
    {
        int startPoint;
        int tempPoint;
        int endPoint;
        int currPoint = currDialogueListPos;

        if(currPoint < dialogueList.Count)
        {
            //Debug.Log("currPoint < dialogueList.Count");
            startPoint = currPoint;
            endPoint = currPoint;
        }
        else
        {
            //Debug.Log("currPoint => dialogueList.Count");
            startPoint = dialogueList.Count - 1;
            endPoint = dialogueList.Count -1 ;
        }

        tempPoint = startPoint;
        if (friendshipPoints == dialogueList[currPoint].getFriendshipPointsReq())
        {
            //Debug.Log("friendshipPoints == dialogueList[currPoint].getFriendshipPointsReq()");
            startPoint = currPoint;
            while ((currPoint + 1 < dialogueList.Count) && (friendshipPoints == dialogueList[currPoint + 1].getFriendshipPointsReq()))
            {
                //Debug.Log("(currPoint + 1 < dialogueList.Count) && (friendshipPoints == dialogueList[currPoint + 1].getFriendshipPointsReq())");
                endPoint += 1;
                currPoint += 1;
            }
        }
        else if (friendshipPoints > dialogueList[currPoint].getFriendshipPointsReq())
        {
            //Debug.Log("friendshipPoints > dialogueList[currPoint].getFriendshipPointsReq()");
            startPoint = currPoint;
            while ((currPoint + 1 < dialogueList.Count) && (friendshipPoints >= dialogueList[currPoint + 1].getFriendshipPointsReq()))
            {
                //Debug.Log("currFriendshipPoints: " + friendshipPoints + " vs " + dialogueList[currPoint + 1].getFriendshipPointsReq());

                //Debug.Log("(currPoint + 1 < dialogueList.Count) && (friendshipPoints > dialogueList[currPoint + 1].getFriendshipPointsReq())");
                if(friendshipPoints > dialogueList[currPoint + 1].getFriendshipPointsReq())
                {
                    //Debug.Log("friendshipPoints > dialogueList[currPoint + 1].getFriendshipPointsReq()");
                    //Debug.Log("currFriendshipPoints: " + friendshipPoints + " vs " + dialogueList[currPoint + 1].getFriendshipPointsReq());
                    startPoint += 1;
                    endPoint += 1;
                    currPoint += 1;
                    //Debug.Log("startPoint: " + startPoint);
                    //Debug.Log("endPoint: " + endPoint);
                    //Debug.Log("currPoint: " + currPoint);

                }
                else if (friendshipPoints == dialogueList[currPoint + 1].getFriendshipPointsReq())
                {
                    //Debug.Log("friendshipPoints == dialogueList[currPoint + 1].getFriendshipPointsReq()");
                    //Debug.Log("currFriendshipPoints: " + friendshipPoints + " vs " + dialogueList[currPoint + 1].getFriendshipPointsReq());
                    startPoint = currPoint + 1;
                    endPoint  = currPoint + 1;
                    currPoint  = currPoint + 1;
                    while ((currPoint + 1 < dialogueList.Count) && (friendshipPoints == dialogueList[currPoint + 1].getFriendshipPointsReq()))
                    {
                        //Debug.Log("(currPoint + 1 < dialogueList.Count) && (friendshipPoints == dialogueList[currPoint + 1].getFriendshipPointsReq())");
                       
                        //Debug.Log("endPoint: " + endPoint);
                        //Debug.Log("currPoint: " + currPoint);
                        endPoint = currPoint + 1;
                        currPoint = currPoint + 1;
                    }
                    //Debug.Log("startPoint: " + startPoint);
                    //Debug.Log("endPoint: " + endPoint);
                    //Debug.Log("currPoint: " + currPoint);

                }
                //else {
                //    Debug.Log("friendshipPoints < dialogueList[currPoint + 1].getFriendshipPointsReq()");
                //    currPoint += 1;
                //}
            }
            currPoint = startPoint;
        }
        else
        {
           //Debug.Log("friendshipPoints < dialogueList[currPoint].getFriendshipPointsReq(): " + (friendshipPoints < dialogueList[currPoint].getFriendshipPointsReq()));
           //Debug.Log("!!! Error Occured since the current friendshipPoints should not be lower than the curr dialogue point");
        }

        currDialogueListPos = startPoint;
        //Debug.Log("currDialogueListPos: " + currDialogueListPos);


        //Debug.Log("startPoint: " + startPoint);
        //Debug.Log("endPoint: " + endPoint);
        //Debug.Log("startPoint Dialogue Section: " + dialogueList[startPoint].getDialogueSection());
        //Debug.Log("endPoint Dialogue Section: " + dialogueList[endPoint].getDialogueSection());

        for(int i = startPoint; i <= endPoint; i++)
        {
            currDialogueList.Add(dialogueList[i]);
        }
        foreach(Dialogue dialogue in currDialogueList)
        {
            //Debug.Log("Dialogue Section in currDialogueList: " +  dialogue.getDialogueSection());
        }

        //Debug.Log("end of CreateCurrentDialogueList() function");
    }


    private void GameStateChangeCheck()
    {
        GameState.NPCChangeCheck(SceneManager.GetActiveScene().name, this.name, friendshipPoints);
    }
}
