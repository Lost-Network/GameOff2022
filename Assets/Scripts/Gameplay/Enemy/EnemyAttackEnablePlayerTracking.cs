using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAttackEnablePlayerTracking : MonoBehaviour
{

    public MovetowardsNearestPlayer projectileScript;
    public float timer;
    public float timerCap = 0.5f;
    private bool didWeEnable = false;

    private void Awake()
    {
        projectileScript = GetComponent<MovetowardsNearestPlayer>();
        projectileScript.enabled = false;
    }
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if(timer<timerCap)
            {
                timer += Time.deltaTime;
            }
            else if(!didWeEnable)
            {
                didWeEnable = true;
                projectileScript.enabled = true;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
