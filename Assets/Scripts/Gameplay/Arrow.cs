using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float timer = 1f;

    public float time = 0f;
    private bool killMe = false;

    // Start is called before the first frame update
    void Start()
    {
        //Attack = this.GetComponentInParent<PlayerStats>().Attack + baseDamage;
        //Debug.Log(Attack);
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ( coll.tag == "Enemy")
        {
            time = 0.15f;
            killMe = true;
        }
        else if (coll.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
}
