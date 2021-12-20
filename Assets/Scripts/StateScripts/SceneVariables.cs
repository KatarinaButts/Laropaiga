using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVariables
{
    string sceneName = "";
    public List<FriendshipState> friendshipStates = new List<FriendshipState>();

    public SceneVariables (string newSceneName)
    {
        sceneName = newSceneName;
        CheckFriendshipStatesFromScene();
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    public bool CheckSceneName(string levelToLoad)
    {
        if(levelToLoad.CompareTo(sceneName) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckFriendshipStatesFromScene()
    {
        NonPlayerCharacter[] NPCSceneArray = Object.FindObjectsOfType<NonPlayerCharacter>();
        FriendshipState friendshipState = null;

        if (NPCSceneArray.Length > 0)   //Check if we have the info for NPCs in the scene
        {
            if (friendshipStates.Count > 0)
            {
                foreach (NonPlayerCharacter c in NPCSceneArray)
                {
                    for (int i = 0; i < friendshipStates.Count; i++)
                    {
                        if (friendshipStates[i].GetNPCName().CompareTo(c.name) == 0)
                        {
                            friendshipStates[i].SetFriendshipAmount(c.getFriendshipPoints());
                        }
                        else
                        {
                            friendshipState = new FriendshipState(c.name, c.getFriendshipPoints()); //Initialize the friendshipState
                            friendshipStates.Add(friendshipState);  //Add it to the list

                        }
                    }
                }
            }
            else
            {
                foreach (NonPlayerCharacter c in NPCSceneArray)
                {
                    //Initialize the friendshipState
                    friendshipState = new FriendshipState(c.name, c.getFriendshipPoints());

                    //Add it to the list
                    friendshipStates.Add(friendshipState);
                }
            }
        }
        else {
            return;
        }
    }

    public void UpdateFriendshipStatesInScene()
    {
        NonPlayerCharacter[] NPCSceneArray = Object.FindObjectsOfType<NonPlayerCharacter>();

        if (NPCSceneArray.Length > 0)   //check if we have the info for NPCs in the scene
        {
            if (friendshipStates.Count > 0)
            {
                foreach (NonPlayerCharacter c in NPCSceneArray)
                {
                    for (int i = 0; i < friendshipStates.Count; i++)
                    {
                        if (friendshipStates[i].GetNPCName().CompareTo(c.name) == 0)
                        {
                            if(friendshipStates[i].GetFriendship() != c.getFriendshipPoints())
                            {
                                c.setFriendshipPoints(friendshipStates[i].GetFriendship());
                            }
                        }
                        else
                        {
                            Debug.Log("Something has gone wrong. Check UpdateFriendshipStatesInScene()");
                        }
                    }
                }
            }
            else
            {
                foreach (NonPlayerCharacter c in NPCSceneArray)
                {
                    FriendshipState friendshipState = null;

                    //initialize the friendshipState
                    friendshipState = new FriendshipState(c.name, c.getFriendshipPoints());

                    //add it to the list
                    friendshipStates.Add(friendshipState);
                }
            }
        }
        else    //there are no NPCs in the scene, so we just return
        {  
            Debug.Log("No NPCs available");
            return;
        }

    }

    public FriendshipState FindFriendshipState(string NPCName)
    {
        FriendshipState checkFriendshipState  = null;

        for (int i = 0; i < friendshipStates.Count; i++)
        {
            if (NPCName.CompareTo(friendshipStates[i].GetNPCName()) == 0)
            {
                return friendshipStates[i];
            }
        }

        return checkFriendshipState;
    }
}
