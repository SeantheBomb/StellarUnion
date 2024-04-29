using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "QueueTarget", menuName = "AIAgent/Target/Queue")]
public abstract class CollectionTarget : AgentTarget
{




    public abstract void Add(Transform t);

    public abstract void Remove(Transform t);

    public abstract bool Contains(Transform t);

    public abstract int Count();

    public abstract void Clear();
}

