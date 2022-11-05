using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThisObject : MonoBehaviour
{
    //This script is not correct
    public float speed;



    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddTorque(speed * Time.deltaTime);
    }
}
