using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnGoalObject", menuName = "Pawn/Goal")]
public class PawnGoalObject : CorruptedValue<PawnGoalData>
{

    //private void OnValidate()
    //{
    //    Value.name = name;
    //}

}


[System.Serializable]
public class PawnGoal : PawnComponentVariable<PawnGoalData>
{

}
