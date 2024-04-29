using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnActionData : PawnGoalData
{

    //public PawnMotor[] requiredMotors;

    //public PawnSensor[] requiredSensors;

    public int ActionPointCost = 1;

    [SerializeReference, SubclassSelector]
    public List<PawnActionCoroutine> actions;


    public override void OnEnable(PawnAgentView view)
    {
        base.OnEnable(view);
        foreach(var consider in desiredConsiders)
        {
            consider.Value.AddAffectingAction(this);
        }
    }

    public override void OnDisable(PawnAgentView view)
    {
        base.OnDisable(view);
        foreach (var consider in desiredConsiders)
        {
            consider.Value.RemoveAffectingAction(this);
        }
    }

    public void DoAction(PawnAgentView view, Action OnComplete = null)
    {
        //Do(view, DoActionCoroutine(view), OnComplete);
        CoroutineEvent tasks = new CoroutineEvent();
        foreach(var a in actions)
        {
            tasks.Add(a.DoAction(view, this));
        }
        tasks.Invoke(view, OnComplete);
    }

    public IEnumerator DoActionCoroutine(PawnAgentView view, Action OnComplete = null)
    {
        yield return view.DoActionTask(this);
        //CoroutineEvent tasks = new CoroutineEvent();
        //foreach (var a in actions)
        //{
        //    tasks.Add(a.DoAction(view, this));
        //}
        //yield return tasks.Play(view, OnComplete);
    }

    //public abstract IEnumerator DoActionCoroutine(PawnAgentView view);


}

[System.Serializable]
public abstract class PawnActionCoroutine
{

    public abstract IEnumerator DoAction(PawnAgentView view, PawnActionData action);

}