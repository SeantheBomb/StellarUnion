using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WithinDistanceTargetPawnConsider : PawnConsiderCondition
{

    [SerializeReference, SubclassSelector]
    public PawnTarget sourceTarget;

    [SerializeReference, SubclassSelector]
    public PawnTarget destinationTarget;

    public float distance;

    public override bool EvaluateCondition(PawnAgentView view)
    {
        float d = Vector3.Distance(sourceTarget.GetTarget(view), destinationTarget.GetTarget(view));
        Debug.Log($"PawnConsiderData: Distance between view {view.name} and target {view.target.name} is {d}. Are we within {distance}? {d <= distance}");
        return  d <= distance;
    }
}
