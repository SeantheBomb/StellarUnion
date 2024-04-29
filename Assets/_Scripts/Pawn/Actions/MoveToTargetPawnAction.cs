using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MoveTowardTargetPawnAction : PawnActionCoroutine
{

    [SerializeReference, SubclassSelector]
    public PawnTarget target;
    public float maxDistance = 1f;
    public float speedMultiplier = 1f;

    public override IEnumerator DoAction(PawnAgentView view, PawnActionData action)
    {
        PawnMovementMotorData movement = action.GetMotor<PawnMovementMotorData>(view);

        yield return movement.MoveToPosition(view, target.GetTarget(view), maxDistance, speedMultiplier);
    }
}

