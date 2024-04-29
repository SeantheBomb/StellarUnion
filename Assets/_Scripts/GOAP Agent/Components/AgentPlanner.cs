using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AgentPlanner
{

    Coroutine actionRunner;

    MinHeap<AgentGoal> goals;

    AgentGoal currentGoal;

    AgentManager manager;
    AgentScheduler scheduler;

    public AgentPlanner(AgentManager manager, AgentScheduler scheduler)
    {
        this.manager = manager;
        this.scheduler = scheduler;

        goals = new MinHeap<AgentGoal>((left, right) =>
        {
            return left.priority > right.priority;
        });
    }


    public void Play()
    {
        actionRunner = manager.StartCoroutine(UpdatePlanner());
    }

    public IEnumerator UpdatePlanner()
    {
        while (goals.Count > 0)
        {
            if (currentGoal == null || currentGoal.IsValid(manager) == false)
            {
                currentGoal = goals.Peek();
            }
            scheduler.QueueActions(CalculateActionPlan(currentGoal));
            yield return new WaitUntil(() => scheduler.HasActionsQueued() == false);
            if (currentGoal.IsValid(manager))
            {
                Debug.Log($"Agent: {manager.name}'s goal {currentGoal} is completed!");
                manager.OnGoalCompleted?.Invoke(currentGoal);
                //goals.Pop(); Eventually we do want to remove completed goals... but not yet!
                currentGoal = null;
            }
            yield return new WaitForSeconds(2f);
        }
        Debug.Log($"Agent: {manager.name} has finished planning!");
    }


    //AgentAction[] FindActionPath(AgentAction start, AgentAction end)
    //{
    //    AgentActionAStar aStar = new AgentActionAStar();
    //    return aStar.CalculateAStarPath(start, end);
    //}

    AgentAction[] GetGoalActions(AgentGoal goal)
    {
        return AgentConsideration.GetAffectingActionable(goal.desiredConsiderations, manager);
    }

    AgentAction[] GetActionPrerequisities(AgentAction action)
    {
        return AgentConsideration.GetAffectingActionable(action.prerequisiteConsiderations, manager);
    }

    AgentAction GetLowestCostPrerequisite(AgentAction action)
    {
        return GetActionPrerequisities(action)/*.Where(a => a.IsActionValid(manager))*/.Min();
    }

    public AgentAction[] CalculateActionPlan(AgentGoal goal)
    {
        List<AgentAction> plan = new List<AgentAction>();
        foreach(AgentAction a in GetGoalActions(goal))
        {
            Debug.Log($"AgentPlanner: {manager.name}'s goal {goal.name} requires {a.name}");
            AddActionToPlan(a, ref plan);
        }
        Debug.Log($"AgentPlanner: {manager.name} has {plan.Count} actions planned");
        return plan.ToArray();
    }

    void AddActionToPlan(AgentAction action, ref List<AgentAction> plan)
    {
        if (plan.Contains(action))
            return;
        //AgentAction lastAction = action;
        if (GetActionPrerequisities(action).Length > 0)
        {
            AgentAction pr = GetLowestCostPrerequisite(action);
            if (pr != null)
            {
                Debug.Log($"AgentPlanner: {manager.name} detected prerequisite for {action.name}");
                AddActionToPlan(pr, ref plan);
                plan.Add(action);
                Debug.Log($"AgentPlanner: {manager.name} Added {action.name} to the plan after {pr.name}");
            }
            else
            {
                Debug.LogWarning($"Agent: {manager.name} has no actionable prerequisites for {action}");
                return;
            }
        }
        else
        {
            plan.Add(action);
            Debug.Log($"AgentPlanner: {manager.name} Added {action.name} to the plan");
        }



    }


    public void AddGoal(AgentGoal goal)
    {
        
            goals.Add(goal);
        
    }

    public void RemoveGoal(AgentGoal goal)
    {
        if (goals.Contains(goal))
        {
            goals.Remove(goal);
        }
    }


}

[System.Serializable]
public class ActionPlan
{
    public AgentAction[] plan;
    public bool isComplete;
    public bool isStale;
}

//public class AgentActionAStar : AStarModel<AgentAction>
//{
//    protected override float CalculateCost(AgentAction from, AgentAction to)
//    {
//        return to.cost;
//    }


//    protected override float CalculateHeuristic(AgentAction node)
//    {
//        float cost = 0;
//        foreach (AgentConsideration ac in AgentAction.GetActionableConsiderations(node.prerequisiteConsiderations))
//        {
//            foreach (AgentAction aa in AgentConsideration.GetAffectingAction(ac))
//            {
//                cost += aa.cost;
//            }
//        }
//        return cost;
//    }
//}

