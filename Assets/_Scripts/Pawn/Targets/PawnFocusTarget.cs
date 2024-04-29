using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnFocusTarget : PawnTarget
{
    public override Vector2 GetTarget(PawnAgentView pawn)
    {
        return pawn.target.position;
    }
}
