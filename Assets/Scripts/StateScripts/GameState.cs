using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameState
{
    //ToDo: Update when we add more forcefields

    #region Forcefields
    //bools for all the forcefields 
    public static bool mainSceneForcefieldToWitchActive = true;
    public static bool mainSceneForcefieldToSnowyMountainActive = true;
    public static bool mainSceneForcefieldToRoyalCityPathActive = true;
    #endregion

    public static bool battleScene = false;

    public static List<SceneVariables> sceneVariables = new List<SceneVariables>();

    public static void SaveSceneVariables(string currScene)
    {
        Debug.Log("GameState sceneVariables.Count" + sceneVariables.Count);
        foreach (SceneVariables s in sceneVariables)
        {
            Debug.Log(s.GetSceneName());
            Debug.Log(s.friendshipStates.Count);

        }

        Debug.Log("Entered SaveSceneVariables(" + currScene + ") function in GameState");
        SceneVariables currSceneVariable = null;

        for (int i = 0; i < sceneVariables.Count; i++)
        {
            if (sceneVariables[i].CheckSceneName(currScene))
            {
                currSceneVariable = sceneVariables[i];
            }
        }
        if (currSceneVariable != null)
        {
            currSceneVariable.CheckFriendshipStatesFromScene();
        }
        else
        {
            currSceneVariable = new SceneVariables(currScene);
            sceneVariables.Add(currSceneVariable);
        }
    }

    public static void LoadSceneVariables(string levelToLoad)
    {
        //Check if this is a battle scene
        CalcBattleSceneBool(levelToLoad);
        //Reset the player's steps
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetSteps();

        Debug.Log("GameState sceneVariables.Count" + sceneVariables.Count);
        foreach(SceneVariables s in sceneVariables)
        {
            Debug.Log(s.GetSceneName());
            Debug.Log(s.friendshipStates.Count);
        }


        Debug.Log("Entered LoadSceneVariables(" + levelToLoad + ") function in GameState");

        SceneVariables currSceneVariable = null;

        for (int i = 0; i < sceneVariables.Count; i++)
        {
            if (sceneVariables[i].CheckSceneName(levelToLoad))
            {
                Debug.Log("sceneVariables[i].CheckSceneName(levelToLoad)");
                currSceneVariable = sceneVariables[i];
            }
        }
        if(currSceneVariable != null)
        {
            Debug.Log("currSceneVariable != null");

            //grab its scene info and change info for variables in the scene accordingly
            currSceneVariable.UpdateFriendshipStatesInScene();
        }
        else
        {
            Debug.Log("currSceneVariable == null : " + (currSceneVariable == null));

            currSceneVariable = new SceneVariables(levelToLoad);
        }
    }

    public static bool GetForcefieldActivatedStatus(string sceneName, string forcefieldName)
    {
        bool isActive = true;
        switch(sceneName)
        {
            case "ForestInsideHouses":
                break;
            case "FrozenCave":
                break;
            case "FrozenCaveRoom2":
                break;
            case "FrozenForest":
                break;
            case "MainScene":
                switch(forcefieldName)
                {
                    case "ForcefieldToWitch":
                        isActive = mainSceneForcefieldToWitchActive;
                        break;
                    case "ForcefieldToSnowyMountain":
                        isActive = mainSceneForcefieldToSnowyMountainActive;
                        break;
                    case "ForcefieldToRoyalCityPath":
                        isActive = mainSceneForcefieldToRoyalCityPathActive;
                        break;
                    default:
                        Debug.Log("No forcefieldName matches");
                        break;
                }
                break;
            case "WitchsForest":
                break;
            default:
                Debug.Log("No sceneName matches");
                break;
        }







        return isActive;
    }

    public static void NPCChangeCheck(string sceneName, string NPCName, int NPCFriendshipPoints)
    {
        switch (sceneName)
        {
            case "ForestInsideHouses":
                break;
            case "FrozenCave":
                break;
            case "FrozenCaveRoom2":
                break;
            case "FrozenForest":
                break;
            case "MainScene":
                switch (NPCName)
                {
                    case "Mouse":
                        if (NPCFriendshipPoints == 2)
                        {
                            mainSceneForcefieldToWitchActive = false;
                            Object[] sceneObjects = SceneManager.GetSceneByName("MainScene").GetRootGameObjects();
                            GameObject forcefield = null;

                            for(int i = 0; i < sceneObjects.Length; i ++)
                            {
                                //Debug.Log("searching for forcefield");

                                if (sceneObjects[i].name.CompareTo("ForcefieldToWitch") == 0)
                                {
                                    //Debug.Log("found forcefield");
                                    forcefield = (GameObject)sceneObjects[i];
                                }
                            }
                            if(forcefield != null)
                            {
                                forcefield.GetComponent<Forcefield>().Inactivate();
                                //Debug.Log("mainSceneForcefieldToWitchActive is now false");

                            }
                        }
                        break;
                    /*
                case "ForcefieldToSnowyMountain":
                    mainSceneForcefieldToSnowyMountainActive;
                    break;
                case "ForcefieldToRoyalCityPath":
                    isActive = mainSceneForcefieldToRoyalCityPathActive;
                    break;
                    */
                    default:
                        Debug.Log("No NPCName matches");
                        break;
                }
                break;
            case "WitchsForest":
                break;
            default:
                Debug.Log("No sceneName matches");
                break;

        }
    }

    public static void CalcBattleSceneBool(string sceneName)
    {
        switch (sceneName)
        {
            case "ForestInsideHouses":
                battleScene = false;
                break;
            case "FrozenCave":
                battleScene = false;
                break;
            case "FrozenCaveRoom2":
                battleScene = false;
                break;
            case "FrozenForest":
                battleScene = false;
                break;
            case "MainScene":
                battleScene = false;
                break;
            case "WitchsForest":
                battleScene = true;
                Debug.Log("***BattleScene***");
                break;
            default:
                Debug.Log("No sceneName matches");
                break;

        }
    }
}
