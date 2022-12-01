using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireArrow : MonoBehaviour
{
    public float timer = 1f;

    public float time = 0f;
    private bool killMe = false;

    public GameObject[] shotDirections;
    private int bulletsShot;
    private bool bigBulletActive;
    private float bigBulletTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Attack = this.GetComponentInParent<PlayerStats>().Attack + baseDamage;
        //Debug.Log(Attack);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        object[] instanceData = new object[1];
        instanceData[0] = 3;
        for (int i = 0; i < shotDirections.Length ; i++)
        {
            GameObject go = PhotonNetwork.Instantiate("FireProjectile", shotDirections[i].transform.position, shotDirections[i].transform.rotation, 0, instanceData);
            go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * 10f);
            bulletsShot++;
        }
        if (bulletsShot >= shotDirections.Length - 1)
        {
            timer = 2;
            bulletsShot = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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

    // void OnTriggerEnter2D(Collider2D coll)
    // {
    //     if ( coll.tag == "Enemy")
    //     {
    //         time = 0.15f;
    //         killMe = true;
    //     }
    //     else if (coll.tag == "Wall")
    //     {
    //         Destroy(gameObject);
    //     }

    // }
}
