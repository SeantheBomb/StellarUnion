using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnThisTarget : PawnTarget
{
    public override Vector2 GetTarget(PawnAgentView pawn)
    {
        return pawn.transform.position;
    }
}
