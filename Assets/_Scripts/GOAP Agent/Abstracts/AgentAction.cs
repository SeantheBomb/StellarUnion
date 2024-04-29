using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentAction : CorruptedModel, IComparable
{
    [Header("Agent Behaviour")]
    public int cost;

    [Header("Affected States")]
    public DesiredConsideration[] affectedConsiderations;

    [Header("Required States")]
    public DesiredConsideration[] prerequisiteConsiderations;

    public void SubscribeAffectors()
    {
        foreach(DesiredConsideration ac in affectedConsiderations)
        {
            AgentConsideration.AddAffectingAction(ac.consideration, this);
        }
    }


    public abstract IEnumerator BeforeActionStart(AgentManager agent, AgentAction previousAction = null);

    public abstract IEnumerator StartAction(AgentManager agent);

    public abstract IEnumerator WhileAction(AgentManager agent);

    public abstract IEnumerator EndAction(AgentManager agent);

    public abstract IEnumerator ActionInterrupted(AgentManager agent);

    public abstract bool IsActionComplete(AgentManager agent);

    public abstract bool IsActionValid(AgentManager agent);

    


    public static AgentConsideration[] GetActionableConsiderations(DesiredConsideration[] considerations, AgentManager manager)
    {
        List<AgentConsideration> list = new List<AgentConsideration>();
        foreach(DesiredConsideration dc in considerations)
        {
            if(dc.IsComplete(manager) == false)
            {
                list.Add(dc.consideration);
            }
        }
        //Debug.Log($"AgentAction: Of {considerations.Length} considerdations {list.Count} are actionable");
        return list.ToArray();
    }



    //public IGraphNode[] GetNeighbours()
    //{
    //    List<IGraphNode> neighbours = new List<IGraphNode>();
    //    foreach (AgentConsideration ac in GetActionableConsiderations(affectedConsiderations)) {
    //        if( AgentConsideration.GetAffectingAction(ac, out AgentAction[]                                 actions)){
    //            neighbours.AddRange(actions);
    //        }
    //    }
    //    return neighbours.ToArray();
    //}

    //public bool IsValid()
    //{
    //    return GetActionableConsiderations(prerequisiteConsiderations).Length == 0;
    //}

    public int CompareTo(object obj)
    {
        if (obj is AgentAction == false)
            return 0;

        AgentAction other = obj as AgentAction;
        return (int)(cost - other.cost);
    }

    //public virtual AgentConsideration[] IsConsiderationsCompleted(AgentManager agent)
    //{

    //}


}

[System.Serializable]
public class DesiredConsideration
{
    public AgentConsideration consideration;
    public bool desiredState;


    public bool IsComplete(AgentManager manager)
    {
        return consideration.GetResult(manager) == desiredState;
    }


    //public void SetState(bool state)
    //{
    //    consideration.SetValue(state);
    //}

    //public void Complete()
    //{
    //    consideration.SetValue(desiredState);
    //}
}
