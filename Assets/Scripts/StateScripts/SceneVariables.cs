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
        Debug.Log("CheckFriendshipStatesFromScene()");

        Debug.Log("GetActiveScene: " + SceneManager.GetActiveScene().name);

        //SceneManager.GetActiveScene().GetRootGameObjects().to    ToList().ForEach(g => results.AddRange(g.GetComponentsInChildren<T>()


        NonPlayerCharacter[] NPCSceneArray = Object.FindObjectsOfType<NonPlayerCharacter>();
        FriendshipState friendshipState = null;

        if (NPCSceneArray.Length > 0)   //check if we have the info for NPCs in the scene
        {
            Debug.Log("NPCSceneArray.Length > 0");
            if (friendshipStates.Count > 0)
            {
                Debug.Log("friendshipStates.Count > 0");

                foreach (NonPlayerCharacter c in NPCSceneArray)
                {
                    for (int i = 0; i < friendshipStates.Count; i++)
                    {
                        if (friendshipStates[i].GetNPCName().CompareTo(c.name) == 0)
                        {
                            friendshipStates[i].SetFriendshipAmount(c.getFriendshipPoints());
                            Debug.Log("NPC Name: " + c.name);
                            Debug.Log("NPC Friendship Points: " + c.getFriendshipPoints());
                        }
                        else
                        {
                            Debug.Log("Initializing NPC info");
                            //initialize the friendshipState
                            friendshipState = new FriendshipState(c.name, c.getFriendshipPoints());

                            //add it to the list
                            friendshipStates.Add(friendshipState);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("friendshipStates.Count == " + friendshipStates.Count);
                foreach (NonPlayerCharacter c in NPCSceneArray)
                {
                    Debug.Log("Initializing NPC info");

                    //initialize the friendshipState
                    friendshipState = new FriendshipState(c.name, c.getFriendshipPoints());

                    //add it to the list
                    friendshipStates.Add(friendshipState);
                }
            }
        }
        else {  //there are no NPCs in the scene, so we just return
            Debug.Log("No NPCs available");

            return;
        }
    }

    public void UpdateFriendshipStatesInScene()
    {
        Debug.Log("UpdateFriendshipStatesInScene()");


        NonPlayerCharacter[] NPCSceneArray = Object.FindObjectsOfType<NonPlayerCharacter>();

        if (NPCSceneArray.Length > 0)   //check if we have the info for NPCs in the scene
        {
            Debug.Log("NPCSceneArray.Length > 0");

            if (friendshipStates.Count > 0)
            {
                Debug.Log("friendshipStates.Count > 0");
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
                            Debug.Log("Something has gone wrong. Check");
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
        else
        {  //there are no NPCs in the scene, so we just return
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

    /*
    void addVariableState(string type, GameObject checkObject)
    {
        switch(type)
        {
            case "NPC":

                break;
            default:
                Debug.Log("Did not find the object type");
                break;
        }
    }
    */
}
