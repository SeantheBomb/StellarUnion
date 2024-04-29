using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnActionScheduler : PawnComponent
{

    PawnActionData previousAction;
    PawnActionData currentAction;

    LinkedList<PawnActionData> actionScheduler = new LinkedList<PawnActionData>();

    PawnAgentView pawnAgent;


    public override void OnEnable(PawnAgentView view)
    {
        base.OnEnable(view);
        pawnAgent = view;
    }

    public void RunActionSchedule(System.Action OnComplete = null)
    {
        CoroutineEvent task = new CoroutineEvent(RunActionScheduleTask());
        task.Invoke(pawnAgent, OnComplete);
    }

    IEnumerator RunActionScheduleTask()
    {
        if (HasActionsQueued() == false)
            yield break;

        if(currentAction == null)
        {
            currentAction = GetNextAction();
            if (currentAction == null)
                yield break;
        }

        if (currentAction.IsValid(pawnAgent) == false)
            yield break;

        yield return DoAction(currentAction);
        previousAction = currentAction;
        currentAction = null;
    }

    IEnumerator DoAction(PawnActionData act)
    {
        PawnActionEvent e = new PawnActionEvent()
        {
            source = pawnAgent,
            action = act
        };
        yield return e.RaiseTask();
    }

    public PawnActionData GetNextAction()
    {
        if (HasActionsQueued())
        {
            var action = actionScheduler.First.Value;
            actionScheduler.RemoveFirst();
            return action;
        }
        return null;
    }

    public void QueueAction(PawnActionData action)
    {
        actionScheduler.AddLast(action);
        //Debug.Log($"PawnAgent: {pawnAgent.name} scheduled action {action.name}");
    }

    public void QueueActions(PawnActionPlan plan)
    {
        //Debug.Log($"PawnAgent: {pawnAgent.name} start scheduling actions count {plan.plan.Length}");
        foreach (PawnActionData a in plan)
        {
            QueueAction(a);
        }
    }

    public void InsertAction(PawnActionData indexAction, PawnActionData newAction, bool beforeIndex = true)
    {
        var index = actionScheduler.Find(indexAction);
        if (beforeIndex)
            actionScheduler.AddBefore(index, newAction);
        else
            actionScheduler.AddAfter(index, newAction);
    }

    public void ClearActions()
    {
        actionScheduler.Clear();
    }



    public bool HasActionsQueued()
    {
        return actionScheduler.Count > 0;
    }


}
