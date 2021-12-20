using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : Interactable
{

    //string characterName = "";
    int friendshipPoints;

    [SerializeField]
    string xmlTextFileName;

    XmlDocument dialogueDataXml;

    private List<Dialogue> dialogueList = new List<Dialogue>();
    private List<Dialogue> currDialogueList = new List<Dialogue>();
    private GameManager gameManager;
    int currDialogueListPos;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        friendshipPoints = 0;
        currDialogueListPos = 0;

        TextAsset xmlTextAsset = Resources.Load<TextAsset>("TextFiles/" + xmlTextFileName);

        CreateDialogueList(xmlTextAsset);

        CreateCurrentDialogueList();
    }

    void Update()
    {
        //Empty
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

        GameStateChangeCheck();     //Checks for game state changes based on new friendship point amount for this NPC

        currDialogueList.Clear();   //Clears the list to prepare for a new list
        CreateCurrentDialogueList();
    }

    public void DisplayDialog()
    {
        gameManager.ManageDialogue(currDialogueList, this.gameObject);
    }

    public override void Interact(Transform PlayerTransform)
    {
        DictionaryManager dictManager = gameManager.GetComponent<DictionaryManager>();
        string searchWord = "hanasu";   //This is the word required to speak with any NPC

        //This checks if you are talking to the first NPC available to speak with, so the word hanasu does not need to be used
        if((SceneManager.GetActiveScene().name.CompareTo("MainScene") == 0) && this.gameObject.name == "Mouse" && friendshipPoints == 0)
        {
            base.Interact(PlayerTransform);

            if (!gameManager.GetDisplayingDialogue())
            {
                DisplayDialog();
            }
        }

        else if (dictManager.GetActiveWord() != null && dictManager.GetActiveWord().GetSearchWord().ToLower().Equals(searchWord))
        {
            base.Interact(PlayerTransform);
            if (!gameManager.GetDisplayingDialogue())
            {
                DisplayDialog();
            }
        }
        else
        {
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
            characterDialogueItem.SetData(dialogueSection);
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
            startPoint = currPoint;
            endPoint = currPoint;
        }
        else
        {
            startPoint = dialogueList.Count - 1;
            endPoint = dialogueList.Count -1 ;
        }

        tempPoint = startPoint;
        if (friendshipPoints == dialogueList[currPoint].GetFriendshipPointsReq())
        {
            startPoint = currPoint;
            while ((currPoint + 1 < dialogueList.Count) && (friendshipPoints == dialogueList[currPoint + 1].GetFriendshipPointsReq()))
            {
                endPoint += 1;
                currPoint += 1;
            }
        }
        else if (friendshipPoints > dialogueList[currPoint].GetFriendshipPointsReq())
        {
            startPoint = currPoint;
            while ((currPoint + 1 < dialogueList.Count) && (friendshipPoints >= dialogueList[currPoint + 1].GetFriendshipPointsReq()))
            {
                if(friendshipPoints > dialogueList[currPoint + 1].GetFriendshipPointsReq())
                {
                    startPoint += 1;
                    endPoint += 1;
                    currPoint += 1;
                }
                else if (friendshipPoints == dialogueList[currPoint + 1].GetFriendshipPointsReq())
                {
                    startPoint = currPoint + 1;
                    endPoint  = currPoint + 1;
                    currPoint  = currPoint + 1;
                    while ((currPoint + 1 < dialogueList.Count) && (friendshipPoints == dialogueList[currPoint + 1].GetFriendshipPointsReq()))
                    {
                        endPoint = currPoint + 1;
                        currPoint = currPoint + 1;
                    }

                }
            }
            currPoint = startPoint;
        }
        else
        {
           Debug.Log("Error: the current friendshipPoints should not be lower than the current dialogue point");
        }

        currDialogueListPos = startPoint;

        for(int i = startPoint; i <= endPoint; i++)
        {
            currDialogueList.Add(dialogueList[i]);
        }
    }


    private void GameStateChangeCheck()
    {
        GameState.NPCChangeCheck(SceneManager.GetActiveScene().name, this.name, friendshipPoints);
    }
}
