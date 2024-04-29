using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentLocalStates
{

    protected Dictionary<string, string> localStates;


    public AgentLocalStates()
    {
        localStates = new Dictionary<string, string>();
    }


    public void SetState(string key, string value)
    {
        if (localStates.ContainsKey(key))
            localStates[key] = value;
        else
            localStates.Add(key, value);
    }

    public bool GetState(string key, out string value)
    {
        if(localStates.ContainsKey(key) == false)
        {
            value = "";
            return false;
        }
        value = localStates[key];
        return true;
    }

    public bool CheckState(string key, string expectedValue)
    {
        if (localStates.ContainsKey(key) == false)
        {
            //Debug.Log("Agent: Local State does not exist");
            return false;
        }
        //Debug.Log("Agent: Local State is " + (localStates[key] == expectedValue));
        return localStates[key] == expectedValue;
    }

    public void ClearStates()
    {
        localStates.Clear();
    }


}