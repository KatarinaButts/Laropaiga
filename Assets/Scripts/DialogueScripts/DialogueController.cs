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
            Debug.Log(dialogueObject.GetDialogueSection());
            Debug.Log(dialogueObject.GetFriendshipPointsReq());
            Debug.Log(dialogueObject.GetWordLearned());
            Debug.Log(dialogueObject.GetFightBool());
            Debug.Log(dialogueObject.GetWinDialogue());
            Debug.Log(dialogueObject.GetLoseDialogue());
        }
    }

    void Start()
    {
        //Empty
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
            characterDialogueItem.SetData(dialogueSection);
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

        if (dialogueList.Count > i + 1)
        {
            testNext = dialogueList[i + 1];
            if (curr.GetFriendshipPointsReq() > currFriendshipPoints)    //we don't have enough friendship points to display, leave the conversation
            {

            }
            else if (testNext.GetFriendshipPointsReq() < friendshipPoints && testNext.GetFriendshipPointsReq() > curr.GetFriendshipPointsReq())
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
