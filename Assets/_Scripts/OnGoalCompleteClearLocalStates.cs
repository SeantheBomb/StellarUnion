using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AgentManager))]
public class OnGoalCompleteClearLocalStates : MonoBehaviour
{

    AgentManager agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AgentManager>();
        agent.OnGoalCompleted += ClearStates;
    }

    void ClearStates(AgentGoal goal)
    {
        agent.localStates.ClearStates();
    }
}
