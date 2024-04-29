using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FocusedTarget", menuName = "AIAgent/Target/Focus")]
public class FocusedTarget : AgentTarget
{
    public override Transform GetTarget(AgentManager manager)
    {
        return manager.target;
    }
}
