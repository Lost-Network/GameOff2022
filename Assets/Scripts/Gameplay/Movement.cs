using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Movement : MonoBehaviourPunCallbacks
{
    public float speed = 1;
    public Rigidbody2D rb;
    public float xBorder = 10;
    public float yBorder = 10;
    PhotonView view;
    Transform obj;
    public bool mine = false;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        obj = this.transform;
        if (view.IsMine)
        {
            mine = true;
        }
    }

    public void Update()
    {
        //If you do not own this player no code below this should run
        if (!view.IsMine || GetComponent<PlayerStats>().CheckPlayerState() == 1)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        Vector3 testVect = obj.transform.position + tempVect;
        //sets the X bounds for the player and ensures they can't leave it
        if (testVect.x > xBorder)
        {
            float temp = xBorder;
            testVect = new Vector3(temp, testVect.y, 0);
        }
        else if (testVect.x <= 1)//xBorder * -1))
        {
            float temp = 1; // (xBorder * -1);
            testVect = new Vector3(temp, testVect.y, 0);
        }

        //sets the Y bounds for the player and ensures they can't leave it
        if (testVect.y >= yBorder)
        {
            float temp = yBorder;
            testVect = new Vector3(testVect.x, temp, 0);
        }
        else if (testVect.y <= 1 ) // (yBorder * -1))
        {
            float temp = 1; // (yBorder * -1);
            testVect = new Vector3(testVect.x, temp, 0);
        }
        //Move the Player
        obj.transform.position = testVect;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            Debug.Log("I am a wall!");
        }
    }

}
