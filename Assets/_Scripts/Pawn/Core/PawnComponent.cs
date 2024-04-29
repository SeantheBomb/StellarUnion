using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PawnComponent
{

    public static Dictionary<PawnAgentData, List<PawnComponent>> components = new Dictionary<PawnAgentData, List<PawnComponent>>();


    public virtual void OnEnable(PawnAgentView view)
    {
        if(components.ContainsKey(view.agent) == false)
        {
            components.Add(view.agent, new List<PawnComponent>());
        }
        components[view.agent].Add(this);
    }

    public virtual void OnDisable(PawnAgentView view)
    {
        components[view.agent].Remove(this);
    }

    public bool TryGetViewComponent<T>(PawnAgentView view, out T component)
    {
        T t = view.GetComponentInChildren<T>();

        if(t != null)
        {
            component = default;
            return false;
        }
        component = t;
        return true;
    }

    public void Do(PawnAgentView view, IEnumerator coroutine, Action onComplete = null)
    {
        CoroutineEvent task = new CoroutineEvent();
        task.Add(coroutine);
        task.Invoke(view, onComplete);
        //view.StartCoroutine(coroutine);
    }

    public static void RaiseComponentEvent(PawnAgentData agent, System.Action<PawnComponent> action)
    {
        foreach(var i in components[agent])
        {
            action?.Invoke(i);
        }
    }


    public T GetMotor<T>(PawnAgentView view) where T : PawnMotorData
    {
        return (T)view.agent.Value.motors.Find((p) => p is T);
    }

    //public static void ForAll(Action<IPawnComponentVariable> action, params PawnComponentList<IPawnComponentVariable>[] lists)
    //{
    //    foreach (var l in lists)
    //    {
    //        l.Foreach(action);
    //    }
    //}
}


[System.Serializable]
public abstract class PawnComponentVariable<T> : CorruptedVariable<T>, IPawnComponentVariable where T : PawnComponent
{
    public PawnComponent Component => Value;

    public K GetValue<K>() where K : T
    {
        return Component as K;
    }

}


public interface IPawnComponentVariable
{
    public PawnComponent Component
    {
        get;
    }
}

//[System.Serializable]
//public class PawnComponentList<T> where T : IPawnComponentVariable
//{
//    List<T> components;

//    public void Foreach(Action<T> action)
//    {
//        foreach(T t in components)
//        {
//            action?.Invoke(t);
//        }
//    }

//    public implicit 
//}