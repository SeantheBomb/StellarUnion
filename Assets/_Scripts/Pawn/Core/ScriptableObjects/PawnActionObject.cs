using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnActionObject", menuName = "Pawn/Action")]
public class PawnActionObject : CorruptedValue<PawnActionData>
{



}


[System.Serializable]
public class PawnAction : PawnComponentVariable<PawnActionData>
{

}
