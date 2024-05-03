using Corrupted;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnAgentObject", menuName = "Pawn/Agent")]
public class PawnAgentObject : CorruptedValue<PawnAgentData>
{

    [Button]
    public void Roll()
    {
        Value.stats = ActionStats.Roll();
    }

}


[System.Serializable]
public class PawnAgent : CorruptedVariable<PawnAgentData>
{


}