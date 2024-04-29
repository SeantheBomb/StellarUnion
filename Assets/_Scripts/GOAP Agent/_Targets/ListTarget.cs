using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ListTarget", menuName = "AIAgent/Target/Collections/List")]
public class ListTarget : CollectionTarget
{

    public List<Transform> list = new List<Transform>();

    public override void Add(Transform t)
    {
        list.Add(t);
    }

    public override Transform GetTarget(AgentManager manager)
    {
        Transform t = list.First();
        return t;
    }

    public override void Remove(Transform t)
    {
        if(Contains(t))
            list.Remove(t);
    }

    public override bool Contains(Transform t)
    {
        return list.Contains(t);
    }

    public override int Count()
    {
        return list.Count;
    }

    public override void Clear()
    {
        list.Clear();
    }
}
