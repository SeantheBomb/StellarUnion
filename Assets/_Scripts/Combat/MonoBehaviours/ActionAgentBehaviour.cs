//using NaughtyAttributes;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using Corrupted;

//public class ActionAgentBehaviour : MonoBehaviour
//{

//    [SerializeField]
//    protected ActionAgent agent;


//    //[SerializeField]
//    protected ActionEncounter currentEncounter => agent.activeEncounter;

//    //[SerializeField]
//    protected ActionTurn agentTurn => currentEncounter.GetAgentTurn(agent);

//    public bool InActiveEncounter => currentEncounter != null && agentTurn != null;


//    // Start is called before the first frame update
//    void OnEnable()
//    {
//    }

//    private void OnDisable()
//    {
//    }

    

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public ActionAgent GetAgent()
//    {
//        return agent;
//    }

//    public void SetAgent(ActionAgent agent)
//    {
//        this.agent = agent;
//    }

//    public void DoAction(ActionData actionData)
//    {
        
//        ActionService.GetService().DoAction(actionData);
//    }

//    public void StartEncounter(ActionAgent[] agents)
//    {
//        if (agents.Contains(agent) == false)
//        {
//            List<ActionAgent> group = agents.ToList();
//            group.Add(agent);
//            ActionService.GetService().StartEncounter(group.ToArray());
//        }
//        else
//        {
//            ActionService.GetService().StartEncounter(agents);
//        }
//    }

//    [Button]
//    public void StartTestEncounter()
//    {
//        StartEncounter(FindObjectsOfType<ActionAgentBehaviour>().GetFromList((aa) => aa.agent));
//    }
//}
