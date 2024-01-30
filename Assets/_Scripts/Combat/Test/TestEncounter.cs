using NaughtyAttributes;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "TestEncounter", menuName = "Combat/Test/Encounter")]
public class TestActionEncounter : ScriptableObject
{

    public ActionEncounter encounter;

    public TestActionAgent[] agents;

    [Button]
    public void StartEncounter()
    {
        encounter = ActionService.GetService().StartEncounter(GetAgents(agents));
    }

    [Button]
    public void StopEncounter()
    {
        encounter.EndEncounter();
    }

    public ActionAgent[] GetAgents(TestActionAgent[] agents)
    {
        ActionAgent[] actionAgents = new ActionAgent[agents.Length];
        for (int i = 0; i < agents.Length; i++)
        {
            actionAgents[i] = agents[i].agent;
        }
        return actionAgents;
    }

}