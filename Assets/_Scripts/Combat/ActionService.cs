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

    public static Action<PawnActionEvent> OnActionStart, OnActionEnd;

    public static Action<ActionEncounter> OnEncounterStart, OnEncounterEnd;

    [SerializeField]
    private ActionEncounter Encounter;


    //public void DoAction(PawnActionEvent action)
    //{
    //    //action.DoAction();
    //    OnAction?.Invoke(action);
    //    //action?.source?.OnDoAction(action);
    //    //action?.target?.OnReceiveAction(action);
    //}

    public ActionEncounter StartEncounter(PawnAgentView[] agents)
    {
        Encounter = new ActionEncounter(agents);
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
public struct PawnActionEvent
{
    public PawnActionData action;
    public PawnAgentView source;
    public PawnAgentView target;

    //public static System.Action<PawnActionEvent> OnAction;

    public void Raise(System.Action OnComplete)
    {
        PawnActionEvent t = this;
        OnComplete += () => ActionService.OnActionEnd(t);
        ActionService.OnActionStart?.Invoke(t);
        action.DoAction(source, OnComplete);
    }

    public IEnumerator RaiseTask()
    {
        ActionService.OnActionStart?.Invoke(this);
        yield return action.DoActionCoroutine(source);
        ActionService.OnActionEnd?.Invoke(this);
    }
}


[Serializable]
public class ActionEncounter
{

    //public ActionService service;

    [SerializeField]ActionTurn[] turnOrder;

    [SerializeField]int currentTurnIndex;

    [SerializeField] ActionTurn currentTurn;

    public Action<ActionTurn> OnStartTurn, OnEndTurn;


    public ActionEncounter(PawnAgentView[] agents)
    {
        //this.service = service;

        foreach(PawnAgentView a in agents)
        {
            a.activeEncounter = this;
        }
        turnOrder = CreateTurnOrder(agents);

        ActionService.OnEncounterStart?.Invoke(this);
        GoToNextTurn();
    }

    public void EndEncounter()
    {
        foreach (ActionTurn a in turnOrder)
        {
            a.agent.activeEncounter = null;
        }
        turnOrder = null;
        ActionService.OnEncounterEnd?.Invoke(this);
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

    //public bool DoAction(PawnActionEvent action)
    //{
    //    if (action.source != currentTurn.agent)
    //        return false;
    //    if (currentTurn.actionPerTurn < currentTurn.actionPointSpent)
    //        return false;
    //    service.DoAction(action);
    //    return true;
    //}

    public bool IsAgentTurn(PawnAgentView agent)
    {
        return agent == currentTurn.agent;
    }

    public ActionTurn GetAgentTurn(PawnAgentView agent)
    {
        foreach(ActionTurn a in turnOrder)
        {
            if (a.agent == agent)
                return a;
        }
        return null;
    }

    public ActionTurn GetCurrentTurn()
    {
        return currentTurn;
    }

    public ActionTurn[] CreateTurnOrder(PawnAgentView[] agents)
    {
        ActionTurn[] turnOrder = new ActionTurn[agents.Length];
        for (int i = 0; i < agents.Length; i++)
        {
            turnOrder[i] = new ActionTurn(agents[i], this);
        }
        turnOrder.OrderByDescending((a) => a.agent.agent.Value.Initiative);
        return turnOrder;
    }

}

[Serializable]
public class ActionTurn
{
    public PawnAgentView agent;
    [SerializeField] ActionEncounter encounter;

    public bool isTurnActive;

    public int actionPerTurn = 1;
    public int actionPointSpent;

    public int actionPointRemaining => actionPerTurn - actionPointSpent;

    public ActionTurn(PawnAgentView agent, ActionEncounter encounter)
    {
        this.agent = agent;
        this.encounter = encounter;
        //ActionService.OnActionStart += OnAction;
    }

    //~ActionTurn()
    //{
    //    ActionService.OnAction -= OnAction;
    //}

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

    //private void OnAction(PawnActionEvent data)
    //{
    //    if (isTurnActive == false)
    //        return;
    //    if (data.source != agent)
    //        return;
    //    actionPointSpent += data.action.ActionPointCost;
    //    if (actionPointSpent >= actionPerTurn)
    //    {
    //        isTurnActive = false;
    //    }
    //}
}

//[Serializable]
//public class ActionAgent
//{
//    public System.Action<ActionData> OnDoAction, OnReceiveAction;

//    public ActionEncounter activeEncounter;
//    public int initiative;
//    public float health = 100;

//}


//[Serializable]
//public abstract class ActionData
//{

//    public System.Action<ActionAgent, ActionAgent> OnDoActionEvent;

//    public ActionAgent source;
//    public ActionAgent target;

//    public int ActionPointCost = 1;


//    public void DoAction(ActionAgent source, ActionAgent target)
//    {
//        OnDoAction(source, target);
//        OnDoActionEvent?.Invoke(source, target);
//    }

//    protected abstract void OnDoAction(ActionAgent source, ActionAgent target);

//}

//[Serializable]
//public class AttackAction : ActionData
//{

//    public float damage = 10;

//    protected override void OnDoAction(ActionAgent source, ActionAgent target)
//    {
//        target.health -= damage;
//    }
//}