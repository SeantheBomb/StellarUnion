using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnWASDOffsetTarget : PawnTarget
{

    public Vector2 W = Vector2.up, A = Vector2.left, S = Vector2.down, D = Vector2.right;

    [SerializeReference, SubclassSelector]
    public PawnTarget origin = new PawnThisTarget();


    public override Vector2 GetTarget(PawnAgentView pawn)
    {
        Vector2 offset = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            offset += W;
        if (Input.GetKey(KeyCode.A))
            offset += A;
        if (Input.GetKey(KeyCode.S))
            offset += S;
        if (Input.GetKey(KeyCode.D))
            offset += D;

        Vector2 position = origin.GetTarget(pawn);

        Vector2 destination = position + offset.normalized;

        Debug.DrawLine(position, destination);

        return destination; 
    }
}
