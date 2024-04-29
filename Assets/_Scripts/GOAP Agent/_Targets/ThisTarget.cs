using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ThisTarget", menuName = "AIAgent/Target/This")]
public class ThisTarget : AgentTarget
{
    public override Transform GetTarget(AgentManager manager)
    {
        return manager.transform;
    }
}
