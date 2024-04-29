using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SetFocusTargetAction", menuName = "AIAgent/Actions/SetFocusTarget")]
public class SetFocusTargetAction : AgentAction
{

    public AgentTarget target;

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
        return agent.target == target.GetTarget(agent);
    }

    public override bool IsActionValid(AgentManager agent)
    {
        return target.GetTarget(agent) != null;
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        agent.target = target.GetTarget(agent);
        Debug.Log($"AgentAction: {name} set {agent.name} target to {agent.target}", agent);
        yield return null;
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield break;
    }
}
