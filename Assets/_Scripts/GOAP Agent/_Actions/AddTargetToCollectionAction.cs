using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddTargetToCollection", menuName ="AIAgent/Actions/Collection/Add")]
public class AddTargetToCollectionAction : AgentAction
{
    [Header("Behaviour")]
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
        return collection.Contains(target.GetTarget(agent));
    }

    public override bool IsActionValid(AgentManager agent)
    {
        return collection != null && collection.Contains(target.GetTarget(agent)) == false;
    }

    public override IEnumerator StartAction(AgentManager agent)
    {
        collection.Add(target.GetTarget(agent));
        yield return null;
    }

    public override IEnumerator WhileAction(AgentManager agent)
    {
        yield return null;
    }
}
