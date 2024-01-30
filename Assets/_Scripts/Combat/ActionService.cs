using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;
using System;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.Linq;
using UnityEditor.PackageManager;

public class ActionService : Service<ActionService>
{

    public Action<ActionData> OnAction;

    public Action<ActionEncounter> OnEncounterStart, OnEncounterEnd;

    [SerializeField]
    private ActionEncounter Encounter;


    public void DoAction(ActionData action)
    {
        action.DoAction();
        OnAction?.Invoke(action);
        action?.source?.OnDoAction(action);
        action?.target?.OnReceiveAction(action);
    }

    public ActionEncounter StartEncounter(ActionAgent[] agents)
    {
        Encounter = new ActionEncounter(this, agents);
        return Encounter;
    }

    public void StopEncounter()
    {
        Encounter.EndEncounter();
    }

    public override void OnInitialize()
    {
        base.OnInitialize();
    }

    public override void StartService()
    {
        base.StartService();
    }

    public override void StopService()
    {
        if(Encounter != null)
            Encounter.EndEncounter();
        base.StopService();
    }


}

[Serializable]
public class ActionEncounter
{

    public ActionService service;

    [SerializeField]ActionTurn[] turnOrder;

    [SerializeField]int currentTurnIndex;

    [SerializeField] ActionTurn currentTurn;

    public Action<ActionTurn> OnStartTurn, OnEndTurn;


    public ActionEncounter(ActionService service, ActionAgent[] agents)
    {
        this.service = service;

        foreach(ActionAgent a in agents)
        {
            a.activeEncounter = this;
        }
        turnOrder = CreateTurnOrder(agents);

        service.OnEncounterStart?.Invoke(this);
        GoToNextTurn();
    }

    public void EndEncounter()
    {
        foreach (ActionTurn a in turnOrder)
        {
            a.agent.activeEncounter = null;
        }
        turnOrder = null;
        service.OnEncounterEnd?.Invoke(this);
    }

    public void GoToNextTurn()
    {
        if(currentTurn != null && currentTurn.isTurnActive)
        {
            currentTurn.OnTurnEnd();
        }
        if(++currentTurnIndex >= turnOrder.Length)
        {
            currentTurnIndex = 0;
        }
        currentTurn = turnOrder[currentTurnIndex];
        if(currentTurn != null && currentTurn.isTurnActive == false)
        {
            currentTurn.OnTurnStart();
        }
    }

    public bool DoAction(ActionData action)
    {
        if (action.source != currentTurn.agent)
            return false;
        if (currentTurn.actionPerTurn < currentTurn.actionPointSpent)
            return false;
        service.DoAction(action);
        return true;
    }

    public bool IsAgentTurn(ActionAgent agent)
    {
        return agent == currentTurn.agent;
    }

    public ActionTurn GetAgentTurn(ActionAgent agent)
    {
        foreach(ActionTurn a in turnOrder)
        {
            if (a.agent == agent)
                return a;
        }
        return null;
    }

    public ActionTurn[] CreateTurnOrder(ActionAgent[] agents)
    {
        ActionTurn[] turnOrder = new ActionTurn[agents.Length];
        for (int i = 0; i < agents.Length; i++)
        {
            turnOrder[i] = new ActionTurn(agents[i], this);
        }
        turnOrder.OrderByDescending((a) => a.agent.initiative);
        return turnOrder;
    }

}

[Serializable]
public class ActionTurn
{
    public ActionAgent agent;
    [SerializeField] ActionEncounter encounter;

    public bool isTurnActive;

    public int actionPerTurn = 1;
    public int actionPointSpent;

    public int actionPointRemaining => actionPerTurn - actionPointSpent;

    public ActionTurn(ActionAgent agent, ActionEncounter encounter)
    {
        this.agent = agent;
        this.encounter = encounter;
        encounter.service.OnAction += OnAction;
    }

    ~ActionTurn()
    {
        encounter.service.OnAction -= OnAction;
    }

    public void OnTurnStart()
    {
        isTurnActive = true;
        encounter.OnStartTurn?.Invoke(this);
        actionPointSpent = 0;
    }

    public void EndTurn()
    {
        if(isTurnActive)
        encounter.GoToNextTurn();
    }

    public void OnTurnEnd()
    {
        isTurnActive = false;
        encounter.OnEndTurn?.Invoke(this);
    }

    private void OnAction(ActionData data)
    {
        if (isTurnActive == false)
            return;
        if (data.source != agent)
            return;
        actionPointSpent += data.ActionPointCost;
        if(actionPointSpent >= actionPerTurn)
        {
            isTurnActive = false;
        }
    }
}

[Serializable]
public class ActionAgent
{
    public System.Action<ActionData> OnDoAction, OnReceiveAction;

    public ActionEncounter activeEncounter;
    public int initiative;
    public float health = 100;

}


[Serializable]
public abstract class ActionData
{

    public System.Action<ActionAgent, ActionAgent> OnDoActionEvent;

    public ActionAgent source;
    public ActionAgent target;

    public int ActionPointCost = 1;


    public void DoAction(ActionAgent source, ActionAgent target)
    {
        OnDoAction(source, target);
        OnDoActionEvent?.Invoke(source, target);
    }

    protected abstract void OnDoAction(ActionAgent source, ActionAgent target);

}

[Serializable]
public class AttackAction : ActionData
{

    public float damage = 10;

    protected override void OnDoAction(ActionAgent source, ActionAgent target)
    {
        target.health -= damage;
    }
}