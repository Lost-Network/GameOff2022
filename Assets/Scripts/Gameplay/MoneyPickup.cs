using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviourPunCallbacks, IPunObservable
{



    [SerializeField]
    private int moneyValue = 10;



    public void SetMoneyValue(int value)
    {
        moneyValue = value;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PhotonView>().AmOwner && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().GainMoney(moneyValue);
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
