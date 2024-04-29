using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SimplePawnActionPlanner : PawnActionPlanner
{

    List<PawnGoalData> goals;

    PawnGoalData currentGoal;

    PawnAgentView agentView;


    public override PawnActionPlan CalculateActionPlan(PawnGoalData currentGoal)
    {
        List<PawnActionData> plan = new List<PawnActionData>();
        foreach(PawnActionData a in GetGoalActions(currentGoal))
        {
            AddActionToPlan(a, ref plan);
        }
        return new PawnActionPlan(plan);
    }

    void AddActionToPlan(PawnActionData action, ref List<PawnActionData> plan)
    {
        if (plan.Contains(action))
            return;

        //Debug.Log($"PawnAgent: {agentView.name} wants to add {action.name} action to plan");


        PawnActionData[] prerequisite = GetActionPrerequisite(action);

        if(prerequisite.Length > 0)
        {
            //Debug.Log($"PawnAgent: {agentView.name} has {prerequisite.Length} prereqs for {action.name} action before it can be added to plan");

            PawnActionData pr = GetLowestCostAction(prerequisite);
            if(pr != default)
            {
                AddActionToPlan(pr, ref plan);
            }
            else
            {
                //Debug.Log($"PawnActionPlanner: {agentView.name} has no way to execute action {action.name}");
                return;
            }
        }
        plan.Add(action);
        //Debug.Log($"PawnAgent: {agentView.name} added {action.name} action to plan");
    }


    PawnActionData[] GetGoalActions(PawnGoalData goal)
    {
        return PawnConsiderData.GetDesiredActions(goal);
    }

    PawnActionData[] GetActionPrerequisite(PawnActionData action)
    {
        return PawnConsiderData.GetRequiredActions(action);
    }

    PawnActionData GetLowestCostAction(PawnActionData[] actions)
    {
        return actions.OrderBy((a) => a.ActionPointCost).FirstOrDefault();
    }

    public override PawnGoalData GetCurrentGoal()
    {
        return currentGoal;
    }

    public override PawnGoalData GetNextGoal()
    {
        return SetCurrentGoal(goals.FirstOrDefault());
    }

    public override bool HasGoals()
    {
        return goals.Where((g)=>g.IsComplete(agentView) == false).Count() > 0;
    }

    public override PawnGoalData SetCurrentGoal(PawnGoalData current)
    {
        return currentGoal = current;
    }

    public override void Setup(PawnAgentView view)
    {
        goals = new List<PawnGoalData>();
        agentView = view;
        foreach(var g in agentView.agent.Value.goals)
        {
            goals.Add(g);
        }
    }
}
