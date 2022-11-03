using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerAttack : MonoBehaviourPunCallbacks //, IPunInstantiateMagicCallback
{
    //public int attack;
    public int damage = 1;
    public float SelfDestructTimer = 1.0f;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void FixedUpdate()
    {
        SelfDestructTimer -= Time.deltaTime;
        if (SelfDestructTimer <= 0)
        {
            if (view.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                // Destroy(this);
            }
        }
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(attack);
    //    }
    //    else
    //    {
    //        this.attack = (int)stream.ReceiveNext();
    //    }
    //}

    //public void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    object[] instantiationData = info.photonView.InstantiationData;
    //    damage = (int)instantiationData[0];

    //    //bug.Log(damage);
    //    // ...
    //}
}
