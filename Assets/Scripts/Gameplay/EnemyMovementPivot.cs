using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyMovementPivot : MonoBehaviour
{
    [SerializeField]
    private Vector2 pivot;


    [SerializeField]
    private float speed = 10;

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (GetComponent<MovetowardsNearestPlayer>().combatState == 2)
        {
            transform.RotateAround(pivot, Vector3.forward, speed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }
        else
        {
            GetNewPivot();
        }
    }

    private void GetNewPivot()
    {
        pivot = new Vector2(this.transform.position.x + 1, this.transform.position.y);
    }

}
