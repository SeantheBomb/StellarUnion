using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnMovementMotorData : PawnMotorData
{

    public float moveSpeed;
    public float stoppingDistance;

    public bool IsMoving;

    public void MoveTo(PawnAgentView view, Vector3 position)
    {
        Do(view, MoveToPosition(view, position));
    }

    public IEnumerator MoveToPosition(PawnAgentView view, Vector3 position, float maxDistance = Mathf.Infinity, float speedMultiplier = 1f)
    {
        Vector3 delta = view.transform.position - position;
        float distance = delta.magnitude;
        if (distance > maxDistance)
        {
            position = Vector3.MoveTowards(view.transform.position, position, maxDistance);
        }
        IsMoving = true;
        while (true) { 
            delta = view.transform.position - position;
            distance = delta.magnitude;

            if (distance <= stoppingDistance)
                break;

            view.transform.position = Vector3.MoveTowards(view.transform.position, position, moveSpeed * speedMultiplier * Time.deltaTime);
            yield return null;
        }
        IsMoving = false;
    }

}

[CreateAssetMenu(fileName = "Movement Motor", menuName = "Pawn/Motor/Movement")]
public class PawnMovementMotor : PawnMotorObject<PawnMovementMotorData>
{

}

[System.Serializable]
public class PawnMovementMotorObject : PawnMotor
{

}
