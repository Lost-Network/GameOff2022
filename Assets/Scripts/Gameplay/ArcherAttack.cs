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

    public int spreadCount = 3;
    public float rotation = 5f;

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

        // Basic Attack
        if (Input.GetMouseButtonDown(0) && timer <= 0)
        {
            object[] instanceData = new object[1];
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
            instanceData[0] = attack;
            GameObject go = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
            go.GetComponent<Rigidbody2D>().AddForce(aimpoint.transform.up * 20f);
            timer = cooldown;
        }
        
        // Skill 1: Spread Shot
        if (Input.GetMouseButtonDown(1) && timerSkill1 <= 0)
        {
            object[] instanceData = new object[1];
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
            instanceData[0] = attack;
            for (int i = 0; i < spreadCount; i++)
            {
                // Modulus to have arrows fan out according to degree in rotation
                if (i % 2 == 0)
                {
                    // center arrow
                    if (i == 0)
                    {
                        GameObject go = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                        go.transform.Rotate(0.0f, 0.0f, 0.0f);
                        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 20f);
                    } 
                    else
                    {
                        GameObject go = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                        go.transform.Rotate(0.0f, 0.0f, rotation * i - rotation);
                        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 20f);
                    }
                }
                else if (i % 2 == 1)
                {
                    GameObject go = PhotonNetwork.Instantiate("BasicArrow", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                    go.transform.Rotate(0.0f, 0.0f, -rotation * i);
                    go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 20f);
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
