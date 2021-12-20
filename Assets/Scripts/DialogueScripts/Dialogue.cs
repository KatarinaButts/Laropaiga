using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Dialogue
{
    string dialogue = "";
    int friendshipPointsReq = 0;

    string wordLearned = "";
    string fightString = "";
    bool fight = false;
    string playerWinDialogue = "";
    string playerLoseDialogue = "";

    public void SetData(XmlNode curItemNode)
    {
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
    public string GetDialogueSection()
    {
        return dialogue;
    }

    public string GetWordLearned()
    {
        return wordLearned;
    }

    public int GetFriendshipPointsReq()
    {
        return friendshipPointsReq;
    }

    public bool GetFightBool()
    {
        return fight;
    }

    public string GetWinDialogue()
    {
        return playerWinDialogue;
    }

    public string GetLoseDialogue()
    {
        return playerLoseDialogue;
    }
}
