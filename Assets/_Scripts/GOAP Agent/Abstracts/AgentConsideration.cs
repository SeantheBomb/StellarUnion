using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AgentConsideration : CorruptedModel 
{

    public static Dictionary<AgentConsideration, List<AgentAction>> affectingActions = new Dictionary<AgentConsideration, List<AgentAction>>();

    //protected bool? result;

    //public virtual bool GetResult()
    //{
    //    if (result.HasValue)
    //        return result.Value;
    //    EvaluateResult();
    //    return result.Value;
    //}

    public virtual bool GetResult(AgentManager manager)
    {
        return RunEvaluation(manager);
    }

    ///// <summary>
    ///// Force this value to equal true or false, and carry out the effect on its impacted values.
    ///// </summary>
    ///// <param name="value"></param>
    ///// <returns>Return true if this succeeds and false if it fails</returns>
    //public abstract bool SetValue(bool value);

    protected abstract bool RunEvaluation(AgentManager manager);

    public static void AddAffectingAction(AgentConsideration ac, AgentAction action)
    {
        //Debug.Log($"AgentConsideration: {action.name} subscribed to {ac.name}");
        if (affectingActions.ContainsKey(ac))
        {
            if (affectingActions[ac].Contains(action))
                return;
            affectingActions[ac].Add(action);
        }
        else
        {
            affectingActions.Add(ac, new List<AgentAction>(new AgentAction[] { action }));
        }
    }

    public static bool GetAffectingAction(AgentConsideration ac, out AgentAction[] actions)
    {
        if (affectingActions.ContainsKey(ac))
        {
            actions = affectingActions[ac].ToArray();
            return true;
        }
        actions = new AgentAction[0];
        return false;
    }

    public static AgentAction[] GetAffectingAction(AgentConsideration ac)
    {
        if (affectingActions.ContainsKey(ac))
        {
            return affectingActions[ac].ToArray();
        }
        Debug.LogError($"AgentConsideration: {ac.name} has no affectors");
        return new AgentAction[0];
    }

    public static AgentAction[] GetAffectingActionable(DesiredConsideration[] ac, AgentManager manager)
    {
        List<AgentAction> actions = new List<AgentAction>();
        foreach (var c in AgentAction.GetActionableConsiderations(ac, manager))
        {
            actions.AddRange(GetAffectingAction(c));
        }
        return actions.ToArray();
    }



}

