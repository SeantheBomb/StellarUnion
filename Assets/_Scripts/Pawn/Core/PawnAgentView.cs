using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAgentView : MonoBehaviour
{

    [HideInInspector] public ActionEncounter activeEncounter;

    public PawnAgent agent;

    public Transform target;

    public PawnActionScheduler scheduler;


    public bool isBusy;


    private void OnEnable()
    {
        agent.Value.OnEnable(this);
        scheduler = new PawnActionScheduler();
        scheduler.OnEnable(this);
    }


    private void OnDisable()
    {
        //ent.Value.OnDisable(this);
    }

    public void DoAction(PawnActionData action, System.Action OnComplete = null)
    {
        if (agent.Value.ProcessAction(action) == false)
            return;
        System.Action DoOnComplete = OnComplete;
        DoOnComplete += () => isBusy = false;

        isBusy = true;
        action.DoAction(this, DoOnComplete);
    }

    public IEnumerator DoActionTask(PawnActionData action)
    {
        if (agent.Value.ProcessAction(action) == false)
            yield break;

        isBusy = true;
        action.DoAction(this, ()=>isBusy = false);
        yield return new WaitWhile(() => isBusy);
    }


    public void RunSchedule(System.Action OnComplete = null)
    {
        scheduler.RunActionSchedule(OnComplete);
    }


    public PawnConsiderData[] GetDesiredConsiders()
    {
        List<PawnConsiderData> consider = new List<PawnConsiderData>();
        foreach (PawnGoal g in agent.Value.goals)
        {
            foreach (PawnConsiderData c in g.Value.desiredConsiders)
            {
                if (c.Evaluate(this) == false && consider.Contains(c) == false)
                {
                    consider.Add(c);
                }
            }
        }
        return consider.ToArray();
    }
}
