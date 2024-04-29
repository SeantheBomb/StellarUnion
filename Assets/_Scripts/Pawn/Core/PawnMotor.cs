using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class PawnMotorData : PawnComponent
{

}


public abstract class PawnMotorObject<T> : CorruptedValue<T> where T : PawnMotorData
{

}


[System.Serializable]
public class PawnMotor : PawnComponentVariable<PawnMotorData>
{

}