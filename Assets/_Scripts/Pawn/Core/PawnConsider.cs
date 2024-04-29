using Corrupted;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class PawnConsiderData
{

    [SerializeReference, SubclassSelector]
    public List<PawnConsiderCondition> conditions;

    public bool Evaluate(PawnAgentView view)
    {
        //return conditions.All((c) => c.EvaluateCondition(view));
        bool result = true;
        foreach(var c in conditions)
        {
            bool r = c.EvaluateCondition(view);
            if (r == false)
                result = false;
            //Debug.Log($"PawnConsiderData: Condition {c.GetType().Name} evluates to {r} for {view.name}");
        }
        return result;
    }


    #region Affectors

    List<PawnActionData> affectingActions = new List<PawnActionData>();

    public void AddAffectingAction(PawnActionData action)
    {
        //Debug.Log($"PawnAgent: Add affecting action {action.name} to this consider");
        affectingActions.Add(action);
    }

    public void RemoveAffectingAction(PawnActionData action)
    {
        affectingActions.RemoveAll((a) => a == action);
    }

    public void ClearAffectingAction()
    {
        affectingActions.Clear();
    }

    public PawnActionData[] GetAffectingAction()
    {
        return affectingActions.ToArray();
    }

    public static PawnActionData[] GetAffectingActionable(PawnAgentView pawn)
    {
        List<PawnActionData> actions = new List<PawnActionData>();
        foreach(var c in pawn.GetDesiredConsiders())
        {
            actions.AddRange(c.GetAffectingAction());
        }
        return actions.Distinct().ToArray();
    }

    public static PawnActionData[] GetDesiredActions(PawnGoalData goal)
    {
        List<PawnActionData> actions = new List<PawnActionData>();
        foreach (var c in goal.desiredConsiders)
        {
            actions.AddRange(c.Value.GetAffectingAction());
        }
        return actions.Distinct().ToArray();
    }

    public static PawnActionData[] GetRequiredActions(PawnGoalData goal)
    {
        List<PawnActionData> actions = new List<PawnActionData>();
        foreach (var c in goal.requiredConsiders)
        {
            actions.AddRange(c.Value.GetAffectingAction());
        }
        return actions.Distinct().ToArray();
    }

    #endregion

}


[System.Serializable]
public abstract class PawnConsiderCondition
{

    public abstract bool EvaluateCondition(PawnAgentView view);

}