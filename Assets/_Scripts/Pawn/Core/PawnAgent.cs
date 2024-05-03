using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PawnAgentData
{

    public static System.Action<PawnAgentData, PawnGoalData> OnGoalCompleted;


    public ActionStats stats;

    [SerializeReference, SubclassSelector]
    public List<PawnSensorData> sensors;

    [SerializeReference, SubclassSelector]
    public List<PawnMotorData> motors;

    public List<PawnGoal> goals;

    //[SerializeReference, SubclassSelector]
    public List<PawnAction> actions;

    [SerializeReference, SubclassSelector]
    protected PawnActionPlanner planner;


    //public int ActionPoints = 10;

    //public int Initiative = 5;

    public void UpdatePlan(PawnAgentView view)
    {
        planner.UpdatePlanner(view, view.scheduler);
    }

    //public void RunSchedule(PawnAgentView view)
    //{
    //    view.scheduler.RunActionSchedule();
    //}

    public bool ProcessAction(PawnActionData action)
    {
        if(action.ActionPointCost <= stats.vitals.ActionPoints)
        {
            stats.vitals.ActionPoints -= action.ActionPointCost;
            //Debug.Log($"PawnAgent: Process Action {action.name} costs {action.ActionPointCost} Action Points");
            return true;
        }
        return false;
    }


    //IPawnComponentVariable[] components => sensors.Union<IPawnComponentVariable>(motors).Union(goals).Union(actions).ToArray();


    public void OnEnable(PawnAgentView view)
    {
        planner.OnEnable(view);
        planner.Setup(view);

        foreach (var c in sensors)
        {
            c.OnEnable(view);
        }
        foreach (var c in motors)
        {
            c.OnEnable(view);
        }
        foreach(var c in goals)
        {
            c.Value.OnEnable(view);
        }
        foreach(var c in actions)
        {
            c.Value.OnEnable(view);
        }
    }

    public void OnDisable(PawnAgentView view)
    {
        planner.OnDisable(view);
        foreach (var c in sensors)
        {
            c.OnDisable(view);
        }
        foreach (var c in motors)
        {
            c.OnDisable(view);
        }
        foreach (var c in goals)
        {
            c.Value.OnDisable(view);
        }
        foreach (var c in actions)
        {
            c.Value.OnDisable(view);
        }
    }



    //public K GetComponent<T,K>(List<T> components) where K : T
    //{
    //    foreach(T t in components)
    //    {
    //        if (t is K k)
    //            return k;
    //    }
    //    return default;
    //}

    //public K[] GetComponents<T,K>(List<T> components) where K : T
    //{
    //    List<K> kk = new List<K>();
    //    foreach(T t in components)
    //    {
    //        if (t is K k)
    //            kk.Add(k);
    //    }
    //    return kk.ToArray();
    //}

}




