using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AgentScheduler
{

    Coroutine actionRunner;

    AgentAction previousAction;
    AgentAction currentAction;
    LinkedList<AgentAction> actionScheduler = new LinkedList<AgentAction>();

    AgentManager manager;


    public AgentScheduler(AgentManager manager)
    {
        this.manager = manager;
    }

    public bool IsRunning => actionRunner != null;

    public void Play()
    {
        actionRunner = manager.StartCoroutine(RunActionSchedule());
    }

    public void Stop()
    {
        if(actionRunner != null)
        {
            manager.StopCoroutine(actionRunner);
            actionRunner = null;
        }
    }


    public IEnumerator RunActionSchedule()
    {
        while (true)
        {
            if (HasActionsQueued() == false)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            if (currentAction == null)
            {
                currentAction = GetNextAction();
                if (currentAction == null)
                    yield break;
            }
            //Debug.Log($"Agent: {currentAction.name} is next in schedule", manager.gameObject);
            yield return RunAction(currentAction);
            previousAction = currentAction;
            currentAction = null;
            yield return new WaitForSeconds(0.5f);
        }
        actionRunner = null;
    }

    IEnumerator RunAction(AgentAction action)
    {
        yield return currentAction.BeforeActionStart(manager, previousAction);
        if(currentAction.IsActionValid(manager) == false)
        {
            yield return currentAction.ActionInterrupted(manager);
            ClearActions();
            //interrupted = true;
            Debug.LogWarning($"AgentScheduler: {action.name} is invalid and was interrupted");
            yield break;
        }
        Debug.Log($"Agent: {manager.name} started running {action.name}", manager.gameObject);
        Coroutine task = manager.StartCoroutine(currentAction.StartAction(manager));
        yield return null;
        bool interrupted = false;
        while (currentAction.IsActionComplete(manager) == false)
        {
            //if (currentAction.IsActionValid(manager) == false)
            //{
            //    if(task != null)manager.StopCoroutine(task);
            //    yield return currentAction.ActionInterrupted(manager);
            //    ClearActions();
            //    //interrupted = true;
            //    Debug.LogWarning($"AgentScheduler: {action.name} is invalid and was interrupted");
            //    yield break;
            //}
            yield return currentAction.WhileAction(manager);
            yield return new WaitForFixedUpdate();
        }
        if (task != null) manager.StopCoroutine(task);
        //if (interrupted == false)
        yield return currentAction.EndAction(manager);
        Debug.Log($"Agent: {manager.name} stopped running {action.name}", manager.gameObject);
    }

    public AgentAction GetNextAction()
    {
        if (HasActionsQueued())
        {
            var action = actionScheduler.First.Value;
            actionScheduler.RemoveFirst();
            return action;
        }
        return null;
    }

    public bool HasActionsQueued()
    {
        return actionScheduler.Count > 0;
    }

    public void QueueAction(AgentAction action)
    {
        //Debug.Log($"AgentScheduler: Added {action.name} action to queue", manager);
        //bool isRunning = HasActionsQueued();
        actionScheduler.AddLast(action);
        //if (IsRunning == false)
        //   Play();
    }

    public void QueueActions(AgentAction[] action)
    {
        //Debug.Log($"AgentScheduler: Added {action.Length} actions to queue", manager);
        foreach (AgentAction a in action)
            QueueAction(a);
    }

    public void InsertAction(AgentAction indexAction, AgentAction newAction, bool beforeIndex = true)
    {
        var index = actionScheduler.Find(indexAction);
        if (beforeIndex)
            actionScheduler.AddBefore(index, newAction);
        else
            actionScheduler.AddAfter(index, newAction);
    }

    public void InsertActions(AgentAction indexAction, AgentAction[] newAction, bool beforeIndex = true)
    {
        var index = actionScheduler.Find(indexAction);
        foreach (AgentAction a in newAction)
        {
            if (beforeIndex)
                actionScheduler.AddBefore(index, a);
            else
                actionScheduler.AddAfter(index, a);
        }
    }

    public void ClearActions()
    {
        actionScheduler.Clear();
    }

}

