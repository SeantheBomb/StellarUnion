using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class PawnTarget 
{

    public abstract Vector2 GetTarget(PawnAgentView pawn);

}
