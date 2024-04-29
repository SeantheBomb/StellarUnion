using Corrupted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentActionPool", menuName = "AIAgent/ActionPool")]
public class ActionPool : CorruptedModel
{

    [SerializeField] protected AgentAction[] _availableActions;

    public AgentAction[] AvailableActions => _availableActions;


    public bool IsActionAvailable(AgentAction action)
    {
        return AvailableActions.Contains(action);
    }

    public void SubscribeAffectors()
    {
        foreach(AgentAction action in _availableActions)
        {
            action.SubscribeAffectors();
        }
    }

}

