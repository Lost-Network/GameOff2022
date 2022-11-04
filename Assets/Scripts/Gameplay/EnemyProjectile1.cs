using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyProjectile1 : MonoBehaviourPunCallbacks, IPunObservable
{

    private int damage = 1;

    private void Start()
    {
        SetDamage(GetComponent<AttackStats>().damage);
    }
    public void SetDamage(int value)
    {
        damage = value;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<PhotonView>().AmOwner)
        {
            coll.gameObject.GetComponent<PlayerStats>().DecreaseHealth(damage);
            photonView.RPC("DestroyMe", RpcTarget.MasterClient);
        }
        //else
        //{
        //    if (GetComponent<PhotonView>().AmOwner)
        //    {
        //        PhotonNetwork.Destroy(gameObject);
        //        //photonView.RPC("DestroyMe", RpcTarget.AllBuffered);
        //    }
        //    return;
        //}
    }
    [PunRPC]
    void DestroyMe()
    {
        //PhotonNetwork.Destroy(gameObject);
        if (GetComponent<PhotonView>().AmOwner)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
