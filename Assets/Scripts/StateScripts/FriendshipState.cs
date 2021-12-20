using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipState
{
    string friendshipName = "";
    int friendshipLevel = 0;

    public FriendshipState(string name, int friendship)
    {
        friendshipName = name;
        friendshipLevel  = friendship;
    }

    public string GetNPCName()
    {
        return friendshipName;
    }

    public int GetFriendship()
    {
        return friendshipLevel;
    }
    public void SetFriendshipAmount(int friendshipAmount)
    {
        friendshipLevel = friendshipAmount;
    }

}
