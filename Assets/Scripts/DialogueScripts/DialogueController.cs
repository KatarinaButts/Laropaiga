using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    Transform dialogueUICanvas;
    [SerializeField]
    Transform dialogueUIPanel;
    //[SerializeField]
    //Transform dialogueUICharImage;
    [SerializeField]
    Text dialogueUIField;
    [SerializeField]
    string xmlTextFileName;

    XmlDocument dialogueDataXml;

    private List<Dialogue> dialogueList = new List<Dialogue>();

    bool displayingDialogue = false;
    int i = 0;
    int currFriendshipPoints = 0;

    void Awake()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("TextFiles/" + xmlTextFileName);
        
        CreateDialogueList(xmlTextAsset);

        //Test
        foreach(Dialogue dialogueObject in dialogueList)
        {
            Debug.Log(dialogueObject.getDialogueSection());
            Debug.Log(dialogueObject.getFriendshipPointsReq());
            Debug.Log(dialogueObject.getWordLearned());
            Debug.Log(dialogueObject.getFightBool());
            Debug.Log(dialogueObject.getWinDialogue());
            Debug.Log(dialogueObject.getLoseDialogue());
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(displayingDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                i += 1;
                DisplayDialogue(currFriendshipPoints);
            }
        }
    }

    void CreateDialogueList(TextAsset xmlTextAsset)
    {
        dialogueDataXml = new XmlDocument();
        dialogueDataXml.LoadXml(xmlTextAsset.text);
        XmlNodeList dialogueSections = dialogueDataXml.SelectNodes("/Dialogue/DialogueSection");
        foreach(XmlNode dialogueSection in dialogueSections)
        {
            Dialogue characterDialogueItem = new Dialogue();
            characterDialogueItem.setData(dialogueSection);
            dialogueList.Add(characterDialogueItem);
        }
    }

    public void DisplayDialogue(int friendshipPoints)
    {
        currFriendshipPoints = friendshipPoints;
        displayingDialogue = true;
        Dialogue curr = dialogueList[i];
        Dialogue testNext;
        bool checkingDialoguePoint = true;

        //grrrr redo later maybe with dialogueController being a part of GameManager...
        while(checkingDialoguePoint == true)
        {
            if (dialogueList.Count <= 1)
            {
                //display the first dialogue
                checkingDialoguePoint = false;
            }
        }

        //test where we should be for the conversation
       



        if (dialogueList.Count > i + 1)
        {
            testNext = dialogueList[i + 1];
            if (curr.getFriendshipPointsReq() > currFriendshipPoints)    //we don't have enough friendship points to display, leave the conversation
            {

            }
            else if (testNext.getFriendshipPointsReq() < friendshipPoints && testNext.getFriendshipPointsReq() > curr.getFriendshipPointsReq())
            {
                curr = testNext;
            }
        }
        else
        {
            testNext = dialogueList[i];
        }

    }
}
