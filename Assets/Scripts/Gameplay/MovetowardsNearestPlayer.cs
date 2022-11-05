using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class MovetowardsNearestPlayer : MonoBehaviour
{
    public GameObject[] players;

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
        players =
            players
                .Concat(GameObject.FindGameObjectsWithTag("Damsel"))
                .ToArray();
        if (players.Length > 0)
        {
            FindClosest();
            if (closestdist > 1f)
            {
                float step = speed * Time.deltaTime;
                transform.position =
                    Vector2
                        .MoveTowards(transform.position,
                        closest.transform.position,
                        step);
            }
        }
    }

    public void FindClosest()
    {
        closestdist = 9999;
        oldDistance = 9999;

        //players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in players)
        {
            //if (g.GetComponent<PlayerHealth>().dead == true)
            //{
            //    //do nothing
            //}
            float dist =
                Vector3.Distance(this.transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                closest = g;
                closestdist = dist;
                oldDistance = dist;
            }
        }
    }
}
