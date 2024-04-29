using NaughtyAttributes;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TestEncounter", menuName = "Combat/Test/Encounter")]
public class TestActionEncounter : ScriptableObject
{

    public ActionEncounter encounter;

    public PawnAgentView[] agents;

    [Button]
    public void StartEncounter()
    {
        encounter = ActionService.GetService().StartEncounter(FindObjectsOfType<PawnAgentView>());
    }

    [Button]
    public void StopEncounter()
    {
        encounter.EndEncounter();
    }

    [Button]
    public void EndCurrentTurn()
    {
        if (encounter == null)
            return;

        encounter.GetCurrentTurn().EndTurn();
    }

    //public PawnAgentView[] GetAgents(PawnAgentView[] agents)
    //{
    //    PawnAgentView[] actionAgents = new PawnAgentView[agents.Length];
    //    for (int i = 0; i < agents.Length; i++)
    //    {
    //        actionAgents[i] = agents[i].agent;
    //    }
    //    return actionAgents;
    //}

}