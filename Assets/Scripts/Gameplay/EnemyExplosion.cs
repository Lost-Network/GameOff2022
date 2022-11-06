using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyExplosion : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private SpriteRenderer topGraphic;
    [SerializeField]
    private SpriteRenderer BottomGraphic;
    [SerializeField]
    private Collider2D hitbox;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float timerCap = 3f;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float removeTimer;
    [SerializeField]
    private float removeTimerCap = 4f;


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
        //Server doesn't need to do this part right?
        if(timer < timerCap)
        {
            timer += Time.deltaTime;
        }
        else
        {
            topGraphic.enabled = false;
            BottomGraphic.enabled = true;
            hitbox.enabled = true;
        }
        //but it prolly needs to do this stuff
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (removeTimer < removeTimerCap)
        {
            removeTimer += Time.deltaTime;
        }
        else
        {
            photonView.RPC("DestroyMe", RpcTarget.MasterClient);
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<PhotonView>().AmOwner)
        {
            coll.gameObject.GetComponent<PlayerStats>().DecreaseHealth(damage);
        }
    }
    [PunRPC]
    void DestroyMe()
    {
        if (GetComponent<PhotonView>().AmOwner)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
