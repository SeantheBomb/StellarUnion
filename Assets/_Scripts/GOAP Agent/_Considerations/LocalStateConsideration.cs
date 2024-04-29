using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LocalStateConsideration", menuName = "AIAgent/Considerations/LocalState")]
public class LocalStateConsideration : AgentConsideration
{

    public StringVariable key;
    public StringVariable expectedValue;

    protected override bool RunEvaluation(AgentManager manager)
    {
        return manager.localStates.CheckState(key, expectedValue);
    }
}
