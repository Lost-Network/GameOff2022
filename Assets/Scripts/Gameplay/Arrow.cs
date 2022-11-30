using Photon.Pun;
using UnityEngine;

public class Arrow : MonoBehaviourPunCallbacks, IPunObservable
{
    public float timer = 1f;

    public float time = 0f;
    private bool killMe = false;

    // Start is called before the first frame update
    private void Start()
    {
        //Attack = this.GetComponentInParent<PlayerStats>().Attack + baseDamage;
        //Debug.Log(Attack);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (time > 0 && killMe)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!GetComponent<PhotonView>().AmOwner)
        { return; }
        if (coll.gameObject.tag == "Enemy")
        {
            time = 0.15f;
            killMe = true;

            photonView.RPC("DestroyMe", RpcTarget.AllBuffered);
            //PhotonNetwork.Destroy(gameObject);
            Debug.Log("Enemy hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!GetComponent<PhotonView>().AmOwner)
        { return; }
        if (coll.gameObject.tag == "Wall")
        {
            photonView.RPC("DestroyMe", RpcTarget.AllBuffered);
            //PhotonNetwork.Destroy(gameObject);
            Debug.Log("Wall hit");
        }
        else
        {
            time = 0.15f;
            killMe = true;
            photonView.RPC("DestroyMe", RpcTarget.AllBuffered);
            //PhotonNetwork.Destroy(gameObject);
            Debug.Log("Last else hit");
        }
    }

    [PunRPC]
    private void DestroyMe()
    {
        //PhotonNetwork.Destroy(gameObject);

        if (!GetComponent<PhotonView>().AmOwner)
        { return; }
        else
        { PhotonNetwork.Destroy(gameObject); }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}