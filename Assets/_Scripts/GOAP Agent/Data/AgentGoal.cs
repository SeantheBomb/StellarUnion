using Corrupted;
using System;

using UnityEngine;

[CreateAssetMenu(fileName = "AgentGoal", menuName = "AIAgent/Goal")]
public class AgentGoal : CorruptedModel
{
    public DesiredConsideration[] desiredConsiderations;
    public float priority;


    public virtual bool IsValid(AgentManager manager)
    {
        foreach(DesiredConsideration dc in desiredConsiderations)
        {
            if (dc.IsComplete(manager) == false)
                return false;
        }
        return true;
    }

}

