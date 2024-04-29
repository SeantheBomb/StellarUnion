using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class PawnActionPlanner : PawnComponent
{


    public abstract void Setup(PawnAgentView view);

    //public void UpdatePlanner(PawnAgentView view, PawnActionScheduler scheduler, System.Action OnComplete = null)
    //{
    //    CoroutineEvent task = new CoroutineEvent(UpdatePlannerTask(view, scheduler));
    //    task.Invoke(view, OnComplete);
    //}

    public void UpdatePlanner(PawnAgentView view, PawnActionScheduler scheduler)
    {
        if (HasGoals() == false)
            return;

        PawnGoalData current = GetCurrentGoal();


        if (current == null || current.IsValid(view) == false)
        {
            current = SetCurrentGoal(GetNextGoal());
        }

        if (current.IsComplete(view))
        {
            PawnAgentData.OnGoalCompleted?.Invoke(view.agent, current);
            SetCurrentGoal(null);
        }
        else
        {
            scheduler.ClearActions();
            scheduler.QueueActions(CalculateActionPlan(current));
        }
    }

    public abstract PawnActionPlan CalculateActionPlan(PawnGoalData currentGoal);

    public abstract bool HasGoals();

    public abstract PawnGoalData GetCurrentGoal();

    public abstract PawnGoalData SetCurrentGoal(PawnGoalData current);

    public abstract PawnGoalData GetNextGoal();


}


[System.Serializable]
public class PawnActionPlan : IEnumerable
{
    public PawnActionData[] plan;

    public PawnActionPlan(List<PawnActionData> data)
    {
        plan = data.ToArray();
    }

    public IEnumerator GetEnumerator()
    {
        return plan.GetEnumerator();
    }
}