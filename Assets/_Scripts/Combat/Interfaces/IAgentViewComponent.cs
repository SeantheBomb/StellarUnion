using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public abstract class IAgentViewComponent<T> : MonoBehaviour where T : ActionData
{

    protected ActionAgentBehaviour agentBehaviour;

    protected void OnEnable()
    {
        if (agentBehaviour == null)
        {
            agentBehaviour = GetComponentInParent<ActionAgentBehaviour>();
        }
        ActionService.GetService().OnAction += OnActionEvent;
        //AgentAction.OnDoActionEvent += OnActionEvent;
    }

    protected void OnDisable()
    {
        ActionService.GetService().OnAction -= OnActionEvent;

        //AgentAction.OnDoActionEvent -= OnActionEvent;
    }

    private void OnActionEvent(ActionData actionData)
    {
        if (agentBehaviour == null)
        {
            return;
        }
        if (actionData is T action)
        {
            if (agentBehaviour.GetAgent() == action.source)
            {
                OnAgentDoAction(action);
            }
            if (agentBehaviour.GetAgent() == action.target)
            {
                OnAgentReceiveAction(action);
            }
        }
    }


    protected abstract void OnAgentDoAction(T action);

    protected abstract void OnAgentReceiveAction(T action);
}


    //internal interface IAgentReceiveAction 
    //{

    //    public void ReceiveAction(ActionAgentBehaviour agent, ActionData actionData);


    //}

    //internal interface IAgentDoAction
    //{

    //    public void DoAction(ActionAgentBehaviour agent, ActionData actionData);


    //}

