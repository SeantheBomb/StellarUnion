using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PawnCombatAction : PawnActionCoroutine
{

    public static OnCombatAction<CombatEvent>


    public string animation;

    public int range;

    public CombatActionArgs args;


    public override IEnumerator DoAction(PawnAgentView view, PawnActionData action)
    {
        
    }

    protected abstract void HandleCombatEvent();

}

[System.Serializable]
public struct CombatActionArgs
{
    public int damage;
}


[System.Serializable]
public struct CombatEvent
{
    public PawnAgentView source;
    public PawnAgentView target;
    public PawnCombatAction action;
}


