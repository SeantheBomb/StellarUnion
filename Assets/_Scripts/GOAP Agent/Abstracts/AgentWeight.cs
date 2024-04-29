using Corrupted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class AgentWeight : CorruptedModel
{

    protected float? result;

    public virtual float GetResult()
    {
        if (result.HasValue)
            return result.Value;
        EvaluateResult();
        return result.Value;
    }

    public virtual float EvaluateResult()
    {
        result = RunEvaluation();
        return result.Value;
    }

    protected abstract float RunEvaluation();


}
