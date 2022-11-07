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
    public float distanceFromPlayerToStop = 1f;
    public float distanceFromPlayerToRetreat = 0f;
    //public int combatState = 0;

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
            if (closestdist > distanceFromPlayerToStop)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, closest.transform.position, step);
                //combatState = 0;
                GetComponent<EnemyStats>().combatState = 0;
            }
            else if(closestdist < distanceFromPlayerToRetreat)
            {
                //If the enemy is supposed to back up from the player, we do that here
                //When backing up, the enemy does not move at their full speed
                float step = (speed * 0.60f) * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, closest.transform.position, -step);
                GetComponent<EnemyStats>().combatState = 1;
                //combatState = 1;
            }
            else
            {
                //combatState = 2;
                GetComponent<EnemyStats>().combatState = 2;
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
