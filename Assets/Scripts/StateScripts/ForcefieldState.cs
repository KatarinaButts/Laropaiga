using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldState
{
    string forcefieldName = "";
    bool isActive = true;

    ForcefieldState(string name, bool active)
    {
        forcefieldName = name;
        isActive = active;
    }

    public string GetForceFieldName()
    {
        return forcefieldName;
    }

    public bool GetisActive()
    {
        return isActive;
    }
    public void SetActive(bool activeState)
    {
        isActive = activeState;
    }

}
