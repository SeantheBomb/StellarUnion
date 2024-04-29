using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WithinRangeConsideration", menuName = "AIAgent/Considerations/WithinRange")]
public class WithinRangeConsideration : AgentConsideration
{

    public AgentTarget target;
    public FloatVariable range;

    protected override bool RunEvaluation(AgentManager manager)
    {
        var dest = target.GetTarget(manager);
        if (dest == null)
            return false;
        float distance = Vector3.Distance(manager.transform.position, dest.position);
        //Debug.Log($"AgentConsideration: {name} is checking distance between {manager.name} and {dest.name} is {distance}");
        return distance < range;
    }
}
