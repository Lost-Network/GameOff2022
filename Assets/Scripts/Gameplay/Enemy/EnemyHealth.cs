using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class EnemyHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public int health = 1;
    public int maxHealth = 10;
    PhotonView view;

    private float knockBackForce = 5f;
    private bool knockBackEnabled = true;
    public bool canMove = true;
    private float hitStunDuration = 1f;
    public int gold = 1;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (view.IsMine)
            {
                die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "pAtk")
        {
            int damage = coll.GetComponent<PlayerAttack>().damage;
            DamagePopup.Create(transform.position, damage);
            if (damage >= health)
            {
                damage = (int)health;
            }
            health -= damage;
            if (coll.GetComponent<PhotonView>().AmOwner)
            {
                Debug.Log("I hit for " + damage);
                Debug.Log("You hit the enemy");
                if(knockBackEnabled == true)
                {
                    knockBackEnabled = false;
                    photonView.RPC("EnemyKnockBack", RpcTarget.MasterClient, coll.transform.position.x, coll.transform.position.y);
                    if(GetComponent<EnemyStats>().isBoss == false)
                    {
                        photonView.RPC("HitStunOn", RpcTarget.MasterClient);
                    }
                    Invoke("EnableKnockBack", 0.3f);
                }
            }
            else
            {
                Debug.Log("Another player hits the enemy");
            }

            //Does it really matter if this desyncs?
            this.GetComponent<EnemyStats>().DamageColorFlash();       
        }
    }

    public void die()
    {
        PlayerStats.money += gold;
        Debug.Log("MY GOLD IS " + PlayerStats.money);
        WaveManager.enemyCount--;
        PhotonNetwork.Destroy(gameObject);
    }

    //This is really laggy for clients
    [PunRPC]
    public void EnemyKnockBack(float xCord, float yCord)
    {
        Vector3 incomingCords = new(xCord, yCord, 0);
        Vector2 dir = (transform.position - incomingCords).normalized;

        //I had to add rigidbodies to enemies for this to work the way I wanted
        GetComponent<Rigidbody2D>().AddForce(dir * knockBackForce, ForceMode2D.Impulse);
    }

    public void EnableKnockBack()
    {
        knockBackEnabled = true;
    }
    [PunRPC]
    public void HitStunOn()
    {
        canMove = false;
        Invoke("HitStunOff", hitStunDuration);
    }
    public void HitStunOff()
    {
        canMove = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            this.health = (int)stream.ReceiveNext();
        }
    }
}
