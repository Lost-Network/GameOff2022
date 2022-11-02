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

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    public void Update()
    {
        //If you do not own this player no code below this should run
        if (!view.IsMine)
        {
            return;
        }

        //Calculate movement based on the input axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.transform.position + tempVect);
        Vector3 testVect = rb.transform.position + tempVect;

        //sets the X bounds for the player and ensures they can't leave it
        if (testVect.x > xBorder)
        {
            float temp = xBorder;
            testVect = new Vector3(temp, testVect.y, 0);
        }
        else if (testVect.x < (xBorder * -1))
        {
            float temp = (xBorder * -1);
            testVect = new Vector3(temp, testVect.y, 0);
        }

        //sets the Y bounds for the player and ensures they can't leave it
        if (testVect.y >= yBorder)
        {
            float temp = yBorder;
            testVect = new Vector3(testVect.x, temp, 0);
        }
        else if (testVect.y <= (yBorder * -1))
        {
            float temp = (yBorder * -1);
            testVect = new Vector3(testVect.x, temp, 0);
        }
        //Move the Player
        rb.MovePosition(testVect);
    }
}
