using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ArcherAttack : MonoBehaviour
{
    public GameObject aimpoint;
    PhotonView view;
    public float cooldown = 0.6f;
    float timer = 0;

    // Start is called before the first frame update
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine || GetComponent<PlayerStats>().CheckPlayerState() == 1)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && timer <= 0)
        {
            object[] instanceData = new object[1];
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
            instanceData[0] = attack;
            GameObject go = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
            go.GetComponent<Rigidbody2D>().AddForce(aimpoint.transform.up * 10f);
            timer = cooldown;
        }

        if (Input.GetKeyDown("f") && timer <= 0 && Application.isEditor)
        {
            PhotonNetwork.Instantiate("God Swipe", aimpoint.transform.position, aimpoint.transform.rotation);
            timer = cooldown;
        }
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
