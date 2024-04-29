using Corrupted;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PawnGoalData : PawnComponent
{
    public string name;

    public int priority;

    public PawnConsider[] requiredConsiders;

    public PawnConsider[] desiredConsiders;

    public bool IsValid(PawnAgentView view)
    {
        return requiredConsiders.All((c) => c.Value.Evaluate(view));
    }

    public bool IsComplete(PawnAgentView view)
    {
        bool result = desiredConsiders.All((c) => c.Value.Evaluate(view));

        //Debug.Log($"PawnGoalData: Is goal {name} complete? {result}");

        return result;
    }
}


