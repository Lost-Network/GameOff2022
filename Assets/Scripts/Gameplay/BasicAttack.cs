using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BasicAttack : MonoBehaviour
{
    public GameObject aimpoint;
    PhotonView view;
    // Basic Attack
    public float cooldown = 0.6f;
    float timer = 0;

    // Knight Skill: Shield Block
    public GameObject Shield;
    public float cooldownSkill1 = 10f;
    float timerSkill1 = 0;

    // Knight Ultimate: Sword Spin
    public GameObject SwordSpin;
    public float cooldownSkill2 = 30f;
    float timerSkill2 = 0;
    public float durationSkill2 = 10f;
    float durationSkill2Timer = 0.0f;

    private float turningSpeed = 20f; // adjust this value for spinning speed (attack up)
    private float moveHorizontal;
    private Vector3 movement;

    public Transform Soundboard;

    private void Awake()
    {
        Soundboard = GameObject.Find("Soundboard/SFX").transform;
    }

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
            GameObject go = PhotonNetwork.Instantiate("Sword Attack", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
            Soundboard.GetChild(1).GetComponent<AudioSource>().Play();
            timer = cooldown;
        }

        // Knight Skill: Shield Block
        if (Input.GetMouseButtonDown(1) && timerSkill1 <= 0)
        {
            Shield.SetActive(true);
            Soundboard.GetChild(2).GetComponent<AudioSource>().Play();
            timerSkill1 = cooldownSkill1;
        }

        // Knight Ultimate: Sword Spin
        if (Input.GetKeyDown("q") && timerSkill2 <= 0)
        {
            SwordSpin.SetActive(true);
            durationSkill2 = 10f;
            Soundboard.GetChild(4).GetComponent<AudioSource>().Play();
            timerSkill2 = cooldownSkill2;
        } 

        if (Input.GetKeyDown("f") && timer <= 0 && Application.isEditor)
        {
            object[] instanceData = new object[1];
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
            instanceData[0] = 999999999;
            PhotonNetwork.Instantiate("God Swipe", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
            timer = cooldown;
        }
    }

    private void FixedUpdate()
    {
        // Basic Attack
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        
        // Knight Skill: Shield Block
        if (timerSkill1 > 0)
        {
            timerSkill1 -= Time.deltaTime;
        } else if (timerSkill1 <= 0)
        {
            Shield.SetActive(false);
        }

        // Knight Ultimate: Sword Spin
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
        } else if (durationSkill2 <= 0)
        {
            SwordSpin.SetActive(false);
        }
    }
}
