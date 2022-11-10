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
            if (damage >= health)
            {
                damage = (int)health;
            }
            health -= damage;
            if (coll.GetComponent<PhotonView>().AmOwner)
            {
                Debug.Log("You hit the enemy");
                if(knockBackEnabled == true)
                {
                    knockBackEnabled = false;
                    photonView.RPC("EnemyKnockBack", RpcTarget.MasterClient, coll.transform.position.x, coll.transform.position.y);
                    Invoke("EnableKnockBack", 0.3f);
                }
            }
            else
            {
                Debug.Log("Another player hits the enemy");
            }
            this.GetComponent<EnemyStats>().DamageColorFlash();
            //this.GetComponent<DamageFlash>().FlashStart();
            //this.GetComponent<FloatingText>().CreateText(this.transform.position, damage);         
        }
    }

    public void die()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    [PunRPC]
    public void EnemyKnockBack(float xCord, float yCord)
    {
        Vector3 incomingCords = new(xCord, yCord, 0);
        Vector2 dir = (transform.position - incomingCords).normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * knockBackForce, ForceMode2D.Impulse);
    }

    public void EnableKnockBack()
    {
        knockBackEnabled = true;
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
