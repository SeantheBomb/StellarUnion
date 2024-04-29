using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompareBool", menuName = "AIAgent/Considerations/CompareBool")]
public class CompareBoolConsideration : AgentConsideration
{

    public BoolVariable value;
    public BoolVariable comparison;

    public Operation operation;

    //public override bool SetValue(bool value)
    //{
    //    if (value)
    //        this.value = comparison;
    //    return true;
    //}

    protected override bool RunEvaluation(AgentManager manager)
    {
        switch (operation)
        {
            case Operation.EqualTo:
                return value == comparison;
            case Operation.NotEqualTo:
                return value != comparison;
        }
        return false;
    }

    public enum Operation
    {
        EqualTo,
        NotEqualTo
    }
}
