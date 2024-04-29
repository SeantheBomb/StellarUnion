using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentTarget : CorruptedModel
{

    public abstract Transform GetTarget(AgentManager manager);


}
