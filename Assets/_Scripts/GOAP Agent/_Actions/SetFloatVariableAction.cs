using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SetIntAction", menuName = "AIAgent/Actions/SetInt")]
public class SetFloatVariableAction : AgentAction
{

    public FloatVariable target;
    public FloatVariable value;
    public FloatVariable delay;


    public override IEnumerator ActionInterrupted(AgentManager agent)
    {
        yield break;
    }

    public override IEnumerator BeforeActionStart(AgentManager agent, AgentAction previousAction = null)
    {
        yield break;
    }

    public override IEnumerator EndAction(AgentManager agent)
    {
        yield break;
    }

    public override bool IsActionComplete(AgentManager agent)
    {
        return target.Value == value.Value;
    }

    public override bool IsActionValid(AgentManager agent)
    {
        return target.HasValue && value.HasValue;
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        yield return new WaitForSeconds(delay);
        target.Value = value.Value;
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield break;
    }
}
