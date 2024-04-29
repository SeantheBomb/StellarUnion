using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompareFloat", menuName = "AIAgent/Considerations/CompareFloat")]
public class CompareFloatConsideration : AgentConsideration
{

    public FloatVariable value;
    public FloatVariable comparison;

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
            case Operation.GreaterThan:
                return value > comparison;
            case Operation.LessThan:
                return value < comparison;
        }
        return false;
    }

    public enum Operation
    {
        EqualTo,
        LessThan,
        GreaterThan
    }
}
