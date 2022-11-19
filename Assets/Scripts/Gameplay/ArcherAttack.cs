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

    public float cooldownSkill1 = 5f;
    float timerSkill1 = 0;

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
            go.GetComponent<Rigidbody2D>().AddForce(aimpoint.transform.up * 20f);
            timer = cooldown;
        }
        
        if (Input.GetMouseButtonDown(1) && timerSkill1 <= 0)
        {
            object[] instanceData = new object[1];
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
            instanceData[0] = attack;
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        GameObject go = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                        go.transform.Rotate(0.0f, 0.0f, -30f);
                        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 20f);
                        break;
                    case 1:
                        GameObject go1 = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                        // go1.transform.Rotate(0.0f, 0.0f, Mathf.Rad2Deg);
                        go1.GetComponent<Rigidbody2D>().AddForce(go1.transform.up * 20f);
                        break;
                    case 2:
                        GameObject go2 = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                        go2.transform.Rotate(0.0f, 0.0f, 30f);
                        go2.GetComponent<Rigidbody2D>().AddForce(go2.transform.up * 20f);
                        break;
                }
            }

            timerSkill1 = cooldownSkill1;
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
        
        if (timerSkill1 > 0)
        {
            timerSkill1 -= Time.deltaTime;
        }
    }
}
