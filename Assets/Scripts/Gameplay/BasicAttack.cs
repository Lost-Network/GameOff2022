using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BasicAttack : MonoBehaviour
{
    public GameObject aimpoint;
    PhotonView view;
    public float cooldown = 0.6f;
    float timer = 0;

    public float cooldownSkill1 = 5f;
    float timerSkill1 = 0;

    public GameObject SwordSpin;
    public float cooldownSkill2 = 30f;
    float timerSkill2 = 0;
    public float durationSkill2 = 10f;
    float durationSkill2Timer = 0.0f;

    private float turningSpeed = 20f; // adjust this value for spinning speed (attack up)
    private float moveHorizontal;
    private Vector3 movement;

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
            GameObject go = PhotonNetwork.Instantiate("Sword Attack", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
            timer = cooldown;
        }

        if (Input.GetMouseButtonDown(1) && timerSkill1 <= 0)
        {

            timerSkill1 = cooldownSkill1;
        }

        if (Input.GetKeyDown("q") && timerSkill2 <= 0)
        {
            SwordSpin.SetActive(true);
            durationSkill2 = 10f;
            timerSkill2 = cooldownSkill2;
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

        if (timerSkill2 > 0)
        {
            timerSkill2 -= Time.deltaTime;
        }

        if (durationSkill2 > 0)
        {
            durationSkill2Timer += Time.deltaTime;
            int seconds = (int)(durationSkill2Timer % 60);
            if (seconds % 1 == 0)
            {
                SwordSpin.transform.Rotate( new Vector3(0, 0, 1 * turningSpeed), Space.Self );
            }
            durationSkill2 -= Time.deltaTime;
        } else 
        {
            SwordSpin.SetActive(false);
        }
    }
}
