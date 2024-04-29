using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MoveVectorAction : PawnActionCoroutine
{

    public Vector2 move;
    public float speedMultiplier = 1f;

    public override IEnumerator DoAction(PawnAgentView view, PawnActionData action)
    {
        PawnMovementMotorData movement = action.GetMotor<PawnMovementMotorData>(view);

        yield return movement.MoveToPosition(view, (Vector2)view.transform.position + move, Mathf.Infinity, speedMultiplier);
    }
}

