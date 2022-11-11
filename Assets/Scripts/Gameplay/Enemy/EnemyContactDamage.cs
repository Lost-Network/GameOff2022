using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyContactDamage : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }
        if (GetComponent<EnemyHealth>().canMove == false)
        {
            return;
        }
        if (coll.gameObject.GetComponent<PhotonView>().AmOwner)
        {
            coll.gameObject.GetComponent<PlayerStats>().DecreaseHealth(GetComponent<EnemyStats>().damage);
        }
    }


}
