using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyMovementPivot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The center of the circle we are moving in, set automatically")]
    private Vector2 pivot;

    [SerializeField]
    [Tooltip("The size of the circle we will move in, bigger number means smaller circle, below 0 might not work correctly")]
    private float pivotDegree = 5;


    [SerializeField]
    [Tooltip("How fast we will pivot")]
    private float pivotSpeed = 120;

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (GetComponent<EnemyHealth>().canMove == false)
        {
            return;
        }
        if (GetComponent<EnemyStats>().combatState == 2)
        {
            transform.RotateAround(pivot, Vector3.forward, pivotSpeed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }
        else
        {
            GetNewPivot();
        }
    }

    private void GetNewPivot()
    {
        pivot = GetVector2();
    }

    private Vector2 GetVector2()
    {
        Vector2 middleVector;
        middleVector.x = this.transform.position.x - GetComponent<EnemyStats>().closest.transform.position.x;
        middleVector.y = this.transform.position.y - GetComponent<EnemyStats>().closest.transform.position.y;
        middleVector.x = this.transform.position.x - (middleVector.x / pivotDegree);
        middleVector.y = this.transform.position.y - (middleVector.y / pivotDegree);
        return middleVector;
    }

}
