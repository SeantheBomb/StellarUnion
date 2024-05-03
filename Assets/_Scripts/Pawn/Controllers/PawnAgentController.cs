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

        view.agent.Value.stats.vitals.ActionPoints = int.MaxValue;

        DoFirstGoal();
    }

    public void OnDisable(PawnAgentController pawn)
    {
    }



    public void StartEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
        this.encounter = encounter;
        view.agent.Value.stats.vitals.ActionPoints = int.MaxValue;
        controller.StopCoroutine(updateLoop);
    }

    public void EndEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
        DoFirstGoal();
    }


    public void StartTurn(ActionTurn turn, PawnAgentController pawn)
    {
        this.turn = turn;
        view.agent.Value.stats.vitals.ActionPoints += view.agent.Value.stats.capabilities.speed;
        DoFirstGoal();
        Debug.Log($"PawnAgent: Agent {view.name} started their turn with 3 action points");
    }

    public void EndTurn(ActionTurn turn, PawnAgentController pawn)
    {
        controller.StopCoroutine(updateLoop);
        pawn.view.scheduler.ClearActions();
        Debug.Log($"PawnAgent: Agent {view.name} ended their turn");
    }



    public void StartAction(PawnActionEvent action, PawnAgentController pawn)
    {
        Debug.Log($"PawnAgent: Agent {view.name} did action {action.action.name} costing {action.action.ActionPointCost} action points with {view.agent.Value.stats.vitals.ActionPoints} remaining");
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
        view.agent.Value.UpdatePlan(view);
        while (view.agent.Value.stats.vitals.ActionPoints > 0)
        {

            if (view.scheduler.HasActionsQueued() == false)
                view.agent.Value.UpdatePlan(view);


            bool isBusy = true;
            view.RunSchedule(() => isBusy = false);

            yield return new WaitForSeconds(0.1f);
            yield return new WaitWhile(() => isBusy);
        }
        //if(turn != null)turn.EndTurn();
    }


}


[System.Serializable]
public class PawnPlayerController : IPawnAgentControl
{
    PawnAgentController controller;

    PawnAgentView view => controller.view;



    public void OnEnable(PawnAgentController pawn)
    {
        controller = pawn;

    }

    public void OnDisable(PawnAgentController pawn)
    {
    }



    public void StartEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
        EndTurnButton.instance.SetInteract(false);
    }

    public void EndEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
    }


    public void StartTurn(ActionTurn turn, PawnAgentController pawn)
    {
        EndTurnButton.OnEndTurn += () => turn.EndTurn();
        EndTurnButton.instance.SetInteract(true);
    }

    public void EndTurn(ActionTurn turn, PawnAgentController pawn)
    {
        EndTurnButton.OnEndTurn = null;
        EndTurnButton.instance.SetInteract(false);
    }




    public void StartAction(PawnActionEvent action, PawnAgentController pawn)
    {
        EndTurnButton.instance.SetInteract(false);
    }

    public void EndAction(PawnActionEvent action, PawnAgentController pawn)
    {
        EndTurnButton.instance.SetInteract(true);
    }


}


[System.Serializable]
public class PawnNPCController : IPawnAgentControl
{
    PawnAgentController controller;

    PawnAgentView view => controller.view;



    public void OnEnable(PawnAgentController pawn)
    {
        controller = pawn;

    }

    public void OnDisable(PawnAgentController pawn)
    {

    }



    public void StartEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {

    }

    public void EndEncounter(ActionEncounter encounter, PawnAgentController pawn)
    {
    }


    public void StartTurn(ActionTurn turn, PawnAgentController pawn)
    {

    }

    public void EndTurn(ActionTurn turn, PawnAgentController pawn)
    {

    }



    public void StartAction(PawnActionEvent action, PawnAgentController pawn)
    {
    }

    public void EndAction(PawnActionEvent action, PawnAgentController pawn)
    {
        if(view.agent.Value.stats.vitals.ActionPoints <= 0 || view.scheduler.HasActionsQueued() == false)
        {
            view.activeEncounter.GetAgentTurn(view).EndTurn();
        }
    }
}
