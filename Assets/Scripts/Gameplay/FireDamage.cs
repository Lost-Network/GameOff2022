using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private float timer = 10f;

    [SerializeField]
    private int damage = 1;

    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (0 > timer)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Enemy")
        {
            return;
        }

        if (coll.gameObject.GetComponent<PhotonView>().AmOwner && coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
