using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviourPunCallbacks, IPunObservable
{



    [SerializeField]
    private int moneyValue = 10;
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    public void SetMoneyValue(int value)
    {
        moneyValue = value;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PhotonView>().AmOwner)
        {
            collision.gameObject.GetComponent<PlayerStats>().GainMoney(moneyValue);
            PhotonNetwork.Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().GainMoney(moneyValue);
            photonView.RPC("DestroyMe", RpcTarget.AllBuffered);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    [PunRPC]
    void DestroyMe()
    {
        if (view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }        
    }
}
