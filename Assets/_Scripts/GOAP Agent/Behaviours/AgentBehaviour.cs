using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentManager))]
public abstract class AgentBehaviour<T> : CorruptedBehaviour<AgentManager,T> where T : AgentBehaviour<T>
{
    
    public AgentManager manager
    {
        get
        {
            if (instanceKey == null)
                instanceKey = GetComponent<AgentManager>();
            return instanceKey;
        }
    }

    Coroutine currentTask;


    public override void Start()
    {
        instanceKey = GetComponent<AgentManager>();
        base.Start();
    }




    protected abstract IEnumerator DoBehaviour();

    protected abstract IEnumerator DoInterruptBehavior();

    IEnumerator BehaviourTask()
    {
        yield return DoBehaviour();
        currentTask = null;
    }

    public IEnumerator PlayBehaviour()
    {
        if (currentTask != null)
            yield return StopBehaviour();
        currentTask = StartCoroutine(BehaviourTask());
        yield return new WaitWhile(()=>currentTask != null);
        currentTask = null;
    }

    public IEnumerator StopBehaviour()
    {
        if (currentTask == null)
            yield break;
        StopCoroutine(currentTask);
        yield return DoInterruptBehavior();
        currentTask = null;
    }


}


