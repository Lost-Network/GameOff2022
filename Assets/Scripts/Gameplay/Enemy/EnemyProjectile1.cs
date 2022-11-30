using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyProjectile1 : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private float timer = 5;
    private int damage = 1;

    private void Start()
    {
        SetDamage(GetComponent<AttackStats>().damage);
    }
    public void SetDamage(int value)
    {
        damage = value;
    }

    private void FixedUpdate()
    {
        if (!GetComponent<PhotonView>().AmOwner)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(GetComponent<EnemyPuddleDropper>())
        { return; }
        else
        {
            if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<PhotonView>().AmOwner)
            {
                coll.gameObject.GetComponent<PlayerStats>().DecreaseHealth(damage);
                photonView.RPC("DestroyMe", RpcTarget.MasterClient);
                this.gameObject.SetActive(false);
            }
        }
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
