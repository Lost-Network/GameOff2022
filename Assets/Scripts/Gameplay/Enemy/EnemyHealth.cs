using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class EnemyHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public int health = 1;
    public int maxHealth = 10;
    PhotonView view;
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
            }
            else
            {
                Debug.Log("Another player hits the enemy");
            }
            //this.GetComponent<DamageFlash>().FlashStart();
            //this.GetComponent<FloatingText>().CreateText(this.transform.position, damage);         
        }
    }

    public void die()
    {
        PhotonNetwork.Destroy(gameObject);
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
