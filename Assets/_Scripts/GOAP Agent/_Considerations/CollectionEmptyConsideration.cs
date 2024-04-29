using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CollectionEmptyConsideration", menuName = "AIAgent/Considerations/CollectionEmpty")]
public class CollectionEmptyConsideration : AgentConsideration
{

    public CollectionTarget collection;

    protected override bool RunEvaluation(AgentManager manager)
    {
        return collection.Count() == 0;
    }
}
