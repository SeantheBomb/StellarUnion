using Corrupted;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PawnConsiderObject", menuName = "Pawn/Consider")]
public class PawnConsiderObject : CorruptedValue<PawnConsiderData>
{

    [Button]
    public void Clear()
    {
        Value.ClearAffectingAction();
    }

}


[System.Serializable]
public class PawnConsider : CorruptedVariable<PawnConsiderData>
{

}
