using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoveTargetFromCollection", menuName = "AIAgent/Actions/Collection/Remove")]

public class RemoveTargetFromCollectionAction : AgentAction
{

    public AgentTarget target;
    public CollectionTarget collection;


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
        return collection.Contains(target.GetTarget(agent)) == false;
    }

    public override bool IsActionValid(AgentManager agent)
    {
        return collection != null && collection.Contains(target.GetTarget(agent));
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        collection.Remove(target.GetTarget(agent));
        yield return null;
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield return null;
    }
}
