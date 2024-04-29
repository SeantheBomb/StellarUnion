using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitTargetAction", menuName = "AIAgent/Actions/HitTarget")]
public class HitTargetAction : AgentAction
{

    public AgentTarget target;
    public FloatValue targetHealth;
    public FloatVariable damage;
    public FloatVariable ammo;

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
        return true;
    }

    public override bool IsActionValid(AgentManager agent)
    {
        return target.GetTarget(agent) != null;
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        Debug.Log($"AgentAction: {agent} hit target {target.GetTarget(agent)} via {name}", agent.gameObject);
        targetHealth.Value -= damage;
        ammo.Value -= 1;
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield break;
    }

 
}
