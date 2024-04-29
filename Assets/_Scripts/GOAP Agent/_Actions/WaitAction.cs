using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WaitAction", menuName = "AIAgent/Actions/Wait")]
public class WaitAction : AgentAction
{

    public DesiredConsideration[] awaitConsiderations;


    public override IEnumerator ActionInterrupted(AgentManager agent)
    {
        yield return null;
    }

    public override IEnumerator BeforeActionStart(AgentManager agent, AgentAction previousAction = null)
    {
        yield return null;
    }

    public override IEnumerator EndAction(AgentManager agent)
    {
        yield return null;
    }

    public override bool IsActionComplete(AgentManager agent)
    {
        if (awaitConsiderations.Length == 0)
            awaitConsiderations = affectedConsiderations;
        foreach (DesiredConsideration dc in awaitConsiderations)
            if (dc.IsComplete(agent) == false)
                return false;
        return true;
    }

    public override bool IsActionValid(AgentManager agent)
    {
        return agent != null && awaitConsiderations != null;
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        yield return null;
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield return null;
    }
}
