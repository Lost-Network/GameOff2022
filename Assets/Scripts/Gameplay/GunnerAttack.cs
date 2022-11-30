using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunnerAttack : MonoBehaviour
{
    public GameObject aimpoint;
    PhotonView view;
    public float cooldown = 0.6f;
    float timer = 0;

    public float cooldownSkill1 = 5f;
    float timerSkill1 = 0;

    public int spreadCount = 3;
    public float rotation = 5f;

    public float cooldownSkill2 = 30f;
    float timerSkill2 = 0;

    public Transform Soundboard;
    public GameObject[] shotDirections;
    private int bulletsShot;
    private bool bigBulletActive;
    private float bigBulletTimer;

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
            if(!bigBulletActive)
            {
                object[] instanceData = new object[1];
                int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk;
                instanceData[0] = attack;
                GameObject go = PhotonNetwork.Instantiate("BasicBullet", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                go.GetComponent<Rigidbody2D>().AddForce(aimpoint.transform.up * 30f);
                Soundboard.GetChild(1).GetComponent<AudioSource>().Play();
                timer = cooldown;
            }
            else
            {
                object[] instanceData = new object[1];
                int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk * 2;
                instanceData[0] = attack;
                GameObject go = PhotonNetwork.Instantiate("BasicBulletBIG", aimpoint.transform.position, aimpoint.transform.rotation, 0, instanceData);
                go.GetComponent<Rigidbody2D>().AddForce(aimpoint.transform.up * 20f);
                Soundboard.GetChild(1).GetComponent<AudioSource>().Play();
                timer = cooldown;
            }
        }

        // Skill 1: Circle shot
        if (Input.GetMouseButtonDown(1) && timerSkill1 <= 0)
        {
            object[] instanceData = new object[1];
            int attack = GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk * 2;
            instanceData[0] = attack;
            Soundboard.GetChild(2).GetComponent<AudioSource>().Play();
            for (int i = 0; i < shotDirections.Length ; i++)
            {
                if(!bigBulletActive)
                {
                    GameObject go = PhotonNetwork.Instantiate("BasicBullet", shotDirections[i].transform.position, shotDirections[i].transform.rotation, 0, instanceData);
                    go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 30f);
                    bulletsShot++;
                }
                else
                {
                    GameObject go = PhotonNetwork.Instantiate("BasicBulletBIG", shotDirections[i].transform.position, shotDirections[i].transform.rotation, 0, instanceData);
                    go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 20f);
                    bulletsShot++;
                }
            }
            if (bulletsShot >= shotDirections.Length - 1)
            {
                timerSkill1 = cooldownSkill1;
                bulletsShot = 0;
            }
        }

        // Skill 2: 
        if (Input.GetKeyDown("q") && timerSkill2 <= 0)
        {
            timerSkill2 = cooldownSkill2;
            bigBulletActive = true;
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

        if (bigBulletTimer < 3 && bigBulletActive)
        {
            bigBulletTimer += Time.deltaTime;
        }
        else
        {
            bigBulletTimer = 0;
            bigBulletActive = false;
        }
    }
}
