using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AgentNavigation : AgentBehaviour<AgentNavigation>
{

    public Vector3 movePosition;

    NavMeshAgent agent;

    public bool IsMoveStale => Vector3.Distance(movePosition, agent.destination) > 0.1f || agent.isPathStale || agent.hasPath;

    public bool HasReachedDestination => agent.remainingDistance < agent.stoppingDistance && IsMoveStale == false;

    public bool IsMoveComplete => HasReachedDestination || IsMoveStale;


    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator GoToDestination(Vector3 destination)
    {
        movePosition = destination;
        yield return PlayBehaviour();
    }

    public IEnumerator GoToTarget(Transform target)
    {
        do
        {
            movePosition = target.position;
            agent.SetDestination(movePosition);
            yield return new WaitForSeconds(1f);
        } while (Vector3.Distance(agent.transform.position, target.position) > 1f);
    }

    protected override IEnumerator DoBehaviour()
    {
        agent.SetDestination(movePosition);
        yield return new WaitUntil(() => Vector3.Distance(agent.transform.position, movePosition) < 1f);
        //if (IsMoveStale)
        //    yield return DoBehaviour();
    }

    protected override IEnumerator DoInterruptBehavior()
    {
        agent.isStopped = true;
        yield break;
    }


}
