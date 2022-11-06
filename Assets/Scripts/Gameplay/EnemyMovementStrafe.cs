using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyMovementStrafe : MonoBehaviour
{
    [SerializeField]
    private float speed = 30f;

    [SerializeField]
    [Tooltip("Does this enemy change the direction it is moving in?")]
    private bool doesEnemyFlip = false;
    private bool flip = false;
    private float flipTimer = 0;
    [SerializeField]
    [Tooltip("If the enemy does flip, how often does it do so?")]
    private float flipTimerCap = 6;

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(GetComponent<MovetowardsNearestPlayer>().combatState == 2)
        {
            transform.RotateAround(GetComponent<MovetowardsNearestPlayer>().closest.transform.position, Vector3.forward, speed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
            if (doesEnemyFlip == true && flipTimer < flipTimerCap)
            {
                flipTimer += Time.deltaTime;
            }
            else if(doesEnemyFlip == true)
            {
                flipTimer = 0;
                if(flip == false)
                {
                    flip = true;
                    speed = -speed;
                }
                else
                {
                    flip = false;
                    speed = Mathf.Abs(speed);
                }
            }
        }

    }
}
