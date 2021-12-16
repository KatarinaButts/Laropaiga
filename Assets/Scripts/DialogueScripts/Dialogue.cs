using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Dialogue/* : MonoBehaviour*/
{
    string dialogue = "";
    int friendshipPointsReq = 0;

    string wordLearned = "";
    string fightString = "";
    bool fight = false;
    string playerWinDialogue = "";
    string playerLoseDialogue = "";

    public void setData(XmlNode curItemNode)
    {
        //characterName = charName;
        dialogue = curItemNode["Text"].InnerText;
        friendshipPointsReq = int.Parse(curItemNode["FriendshipPointsNeeded"].InnerText);
        wordLearned = curItemNode["Word"].InnerText;
        fightString = curItemNode["Fight"].InnerText;
        if(fightString.ToLower() == "true")
        {
            fight = true;
        }
        else
        {
            fight = false;
        }
        playerWinDialogue = curItemNode["WinDialogue"].InnerText;
        playerLoseDialogue = curItemNode["LoseDialogue"].InnerText;
    }
    public string getDialogueSection()
    {
        return dialogue;
    }

    public string getWordLearned()
    {
        return wordLearned;
    }

    public int getFriendshipPointsReq()
    {
        return friendshipPointsReq;
    }

    public bool getFightBool()
    {
        return fight;
    }

    public string getWinDialogue()
    {
        return playerWinDialogue;
    }

    public string getLoseDialogue()
    {
        return playerLoseDialogue;
    }
}
