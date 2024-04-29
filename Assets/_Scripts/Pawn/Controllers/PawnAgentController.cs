using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PawnAgentView))]
public class PawnAgentController : ComponentInterface<IPawnAgentControl>
{
    public PawnAgentView view
    {
        get; protected set;
    }

    //[SerializeReference, SubclassSelector]
    //public IPawnAgentControl[] controller;



    private void OnEnable()
    {
        view = GetComponent<PawnAgentView>();
        ActionService.OnEncounterStart += StartEncounter;
        ActionService.OnEncounterEnd += EndEncounter;
        ActionService.OnActionStart += StartAction;
        ActionService.OnActionEnd += EndAction;
        SendInterface((c) => c.OnEnable(this));
    }

    private void OnDisable()
    {
        ActionService.OnEncounterStart -= StartEncounter;
        ActionService.OnEncounterEnd -= EndEncounter;
        ActionService.OnActionStart -= StartAction;
        ActionService.OnActionEnd -= EndAction;
        SendInterface((c) => c.OnDisable(this));
    }

    private void StartAction(PawnActionEvent action)
    {
        if (action.source == view)
            SendInterface((c) => c.StartAction(action, this));
    }

    private void EndAction(PawnActionEvent action)
    {
        if (action.source == view)
            SendInterface((c) => c.EndAction(action, this));
    }

    public void StartEncounter(ActionEncounter encounter)
    {
        encounter.OnStartTurn += StartTurn;
        encounter.OnEndTurn += EndTurn;
        SendInterface((c) => c.StartEncounter(encounter, this));
    }

    public void EndEncounter(ActionEncounter encounter)
    {
        encounter.OnStartTurn -= StartTurn;
        encounter.OnEndTurn -= EndTurn;
        SendInterface((c) => c.EndEncounter(encounter, this));
    }

    public void StartTurn(ActionTurn turn)
    {
        if(turn.agent == view)
            SendInterface((c) => c.StartTurn(turn, this));
    }

    public void EndTurn(ActionTurn turn)
    {
        if (turn.agent == view)
            SendInterface((c) => c.EndTurn(turn, this));
    }


    //void SendInterface(System.Action<IPawnAgentControl> action)
    //{
    //    foreach(var c in controller)
    //    {
    //        action?.Invoke(c);
    //    }
    //}

}


public interface IPawnAgentControl
{

    public void OnEnable(PawnAgentController pawn);

    public void OnDisable(PawnAgentController pawn);

    public void StartEncounter(ActionEncounter encounter, PawnAgentController pawn);

    public void EndEncounter(ActionEncounter encounter, PawnAgentController pawn);

    public void StartTurn(ActionTurn turn, PawnAgentController pawn);

    public void EndTurn(ActionTurn turn, PawnAgentController pawn);

    public void StartAction(PawnActionEvent action, PawnAgentController pawn);

    public void EndAction(PawnActionEvent action, PawnAgentController pawn);

}

[System.Serializable]
public class PawnAgentTestControl : IPawnAgentControl
{

    PawnAgentController controller;

    PawnAgentView view => controller.view;

    ActionEncounter encounter;

    ActionTurn turn;

    Coroutine updateLoop;

    public void OnEnable(PawnAgentController pawn)
    {
        controller = pawn;

        view.agent.Value.ActionPoints = int.MaxValue;

        DoFirstGoal();
    }

    public void OnDisable(PawnAgentController pawn)
    {
    }



    public void StartEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
        this.encounter = encounter;
        view.agent.Value.ActionPoints = int.MaxValue;
        controller.StopCoroutine(updateLoop);
    }

    public void EndEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
        DoFirstGoal();
    }


    public void StartTurn(ActionTurn turn, PawnAgentController pawn)
    {
        this.turn = turn;
        view.agent.Value.ActionPoints = 3;
        DoFirstGoal();
    }

    public void EndTurn(ActionTurn turn, PawnAgentController pawn)
    {
        controller.StopCoroutine(updateLoop);
    }



    public void StartAction(PawnActionEvent action, PawnAgentController pawn)
    {
    }

    public void EndAction(PawnActionEvent action, PawnAgentController pawn)
    {
    }


    ////[Button]
    //public void DoFirstAction()
    //{
    //    view.DoAction(view.agent.Value.actions.First());
    //}


    //[Button]
    public void DoFirstGoal()
    {
        updateLoop = controller.StartCoroutine(HandleGoal());
        //StartCoroutine(HandlePlanner());
        //StartCoroutine(HandleSchedule());
    }


    IEnumerator HandleGoal()
    {
        yield return new WaitUntil(() => view.scheduler != null);
        while (view.agent.Value.ActionPoints > 0)
        {

            if (view.scheduler.HasActionsQueued() == false)
                view.agent.Value.UpdatePlan(view);

            bool isBusy = true;
            view.RunSchedule(() => isBusy = false);

            yield return new WaitWhile(() => isBusy);
        }
        //if(turn != null)turn.EndTurn();
    }


}
