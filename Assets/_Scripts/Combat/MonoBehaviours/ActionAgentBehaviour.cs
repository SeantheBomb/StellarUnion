using Assets._Scripts.Combat.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionAgentBehaviour : MonoBehaviour
{

    [SerializeField]
    protected ActionAgent agent;


    [SerializeField]
    protected ActionEncounter currentEncounter;

    [SerializeField]
    protected ActionTurn agentTurn;

    public bool InActiveEncounter => currentEncounter != null && agentTurn != null;


    // Start is called before the first frame update
    void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public ActionAgent GetAgent()
    {
        return agent;
    }

    public void SetAgent(ActionAgent agent)
    {
        this.agent = agent;
    }

    public void DoAction(ActionData actionData)
    {
        
        ActionService.GetService().DoAction(actionData);
    }

    public void StartEncounter(ActionAgent[] agents)
    {
        if (agents.Contains(agent) == false)
        {
            List<ActionAgent> group = agents.ToList();
            group.Add(agent);
            ActionService.GetService().StartEncounter(group.ToArray());
        }
        else
        {
            ActionService.GetService().StartEncounter(agents);
        }
    }
}
