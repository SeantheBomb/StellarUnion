using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "QueueTarget", menuName = "AIAgent/Target/Collections/Queue")]
public class QueueTarget : CollectionTarget
{

    public List<Transform> queue = new List<Transform>();

    public override void Add(Transform t)
    {
        queue.Add(t);
    }

    public override Transform GetTarget(AgentManager manager)
    {
        if (queue.Count == 0)
            return null;
        Transform t = queue.First();
        if (t == null)
            return null;
        queue.Remove(t);
        return t;
    }

    public override void Remove(Transform t)
    {
        if(Contains(t))
            queue.Remove(t);
    }

    public override bool Contains(Transform t)
    {
        return queue.Contains(t);
    }

    public override int Count()
    {
        return queue.Count;
    }

    public override void Clear()
    {
        queue.Clear();
    }
}
