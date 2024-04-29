using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TagTarget", menuName = "AIAgent/Target/Tag")]
public class TagTarget : AgentTarget
{

    public StringVariable tag;

    Transform target;

    public override Transform GetTarget(AgentManager manager)
    {
        if (target == null)
            target = FindTarget();
        return target;
    }

    Transform FindTarget()
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag.Value);
        if (go == null)
        {
            Debug.LogError($"AgentTarget: {name} failed to find target with tag {tag.Value}", this);
            return null;
        }
        return go.transform;
    }
}
