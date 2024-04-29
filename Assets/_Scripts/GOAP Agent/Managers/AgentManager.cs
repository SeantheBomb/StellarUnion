using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AgentManager : CorruptedBehaviour
{

    //public ActionAgent agent;

    public Transform target;

    public AgentGoal currentGoal;
    public ActionPool availableActions;

    public AgentLocalStates localStates
    {
        get; protected set;
    }

    public System.Action<AgentGoal> OnGoalCompleted;

    AgentScheduler scheduler;
    AgentPlanner planner;


    private void Awake()
    {
        scheduler = new AgentScheduler(this);
        planner = new AgentPlanner(this, scheduler);
        localStates = new AgentLocalStates();
        availableActions.SubscribeAffectors();
        //agent.OnDoAction += ScheduleAction;
    }


    private void OnEnable()
    {
        planner.AddGoal(currentGoal);
        Invoke("Play", 0.1f);
    }

    public void Play()
    {
        planner.Play();
        scheduler.Play();
    }



}

