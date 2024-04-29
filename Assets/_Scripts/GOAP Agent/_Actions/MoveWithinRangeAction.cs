using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveWithinRange", menuName = "AIAgent/Actions/MoveWithinRange")]
public class MoveWithinRangeAction : AgentAction
{

    public AgentTarget target;


    public override IEnumerator ActionInterrupted(AgentManager agent)
    {
        yield return AgentNavigation.GetInstance(agent).StopBehaviour();
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
        //AgentNavigation nav = AgentNavigation.GetInstance(agent);
        return Vector3.Distance(agent.transform.position, agent.target.position) < 1f;
    }

    public override bool IsActionValid(AgentManager agent)
    {
        AgentNavigation nav = AgentNavigation.GetInstance(agent);
        return nav != null /*&& nav.IsMoveStale == false*/;
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        agent.target = target.GetTarget(agent);
        yield return AgentNavigation.GetInstance(agent).GoToDestination(agent.target.position);
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield break;
    }
}
