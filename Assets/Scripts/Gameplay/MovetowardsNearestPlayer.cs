using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovetowardsNearestPlayer : MonoBehaviour
{
    GameObject[] players;
    float closestdist;
    float oldDistance;
    public GameObject closest;
    public float speed = 1;

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            FindClosest();
            if (closestdist > 1f)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, closest.transform.position, step);
            }

        }
    }

    private void FindClosest()
    {
        closestdist = 9999;
        oldDistance = 9999;
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject g in players)
        {
            //if (g.GetComponent<PlayerHealth>().dead == true)
            //{
            //    //do nothing
            //}
            float dist = Vector3.Distance(this.transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                closest = g;
                closestdist = dist;
                oldDistance = dist;
            }

        }
    }

}
