using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunnerAttack : MonoBehaviour
{
    //Gunner attacks faster than archer but does less damage
    public GameObject aimpoint;
    PhotonView view;
    public float cooldown = 0.2f;
    float timer = 0;
    float launchSpeed = 30f;

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
            //Bullets get launched faster but do less damage
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
            instanceData[0] = attack;
            GameObject go = PhotonNetwork.Instantiate("BasicBullet", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
            go.GetComponent<Rigidbody2D>().AddForce(aimpoint.transform.up * launchSpeed);
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
