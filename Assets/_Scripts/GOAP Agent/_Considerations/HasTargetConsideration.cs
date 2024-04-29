using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HasTargetConsideration", menuName = "AIAgent/Considerations/HasTarget")]
public class HasTargetConsideration : AgentConsideration
{

    public AgentTarget target;

    protected override bool RunEvaluation(AgentManager manager)
    {
        return target.GetTarget(manager) != null;
    }
}
