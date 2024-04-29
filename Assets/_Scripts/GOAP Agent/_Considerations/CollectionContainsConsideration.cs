using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CollectionContainsConsideration", menuName = "AIAgent/Considerations/CollectionContains")]
public class CollectionContainsConsideration : AgentConsideration
{

    public CollectionTarget collection;
    public AgentTarget target;

    protected override bool RunEvaluation(AgentManager manager)
    {
        return collection.Contains(target.GetTarget(manager));
    }
}
