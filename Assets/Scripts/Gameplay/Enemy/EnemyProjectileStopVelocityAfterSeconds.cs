using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyProjectileStopVelocityAfterSeconds : MonoBehaviour
{

    //Use this to stop projectiles in their tracks, IE an enemy leaves stuff on the floor for the player to dodge
    public float timer;
    public float timerCap = 0.5f;
    private bool didWeEnable = false;
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if (timer < timerCap)
            {
                timer += Time.deltaTime;
            }
            else if (!didWeEnable)
            {
                didWeEnable = true;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
