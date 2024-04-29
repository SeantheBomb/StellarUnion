using Corrupted;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnAgentObject", menuName = "Pawn/Agent")]
public class PawnAgentObject : CorruptedValue<PawnAgentData>
{



}


[System.Serializable]
public class PawnAgent : CorruptedVariable<PawnAgentData>
{

}