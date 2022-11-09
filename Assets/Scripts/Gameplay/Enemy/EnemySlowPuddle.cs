using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowPuddle : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private float timer = 10f;

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
        if (coll.gameObject.tag != "Player")
        {
            return;
        }

        if (coll.gameObject.GetComponent<PhotonView>().AmOwner && coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Movement>().speed = 0.5f;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }

        if (coll.gameObject.GetComponent<PhotonView>().AmOwner && coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Movement>().speed = 2;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
