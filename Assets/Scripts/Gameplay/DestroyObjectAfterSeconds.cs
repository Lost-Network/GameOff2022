using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyObjectAfterSeconds : MonoBehaviour, IPunObservable
{

    public float destroyTimer = 5f;
    private float timer = 0f;
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else if(timer < destroyTimer)
        {
            timer += Time.deltaTime;
        }
        else
        {
            //photonView.RPC("DestroyMe", RpcTarget.MasterClient);
        }
         
    }
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
