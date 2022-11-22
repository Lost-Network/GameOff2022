using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class EnemyBossMovement : MonoBehaviour
{
    //public GameObject[] players;

    float closestdist;

    float oldDistance;

    //public GameObject closest;

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
        GetComponent<EnemyStats>().players = GameObject.FindGameObjectsWithTag("Player");
        GetComponent<EnemyStats>().players =
            GetComponent<EnemyStats>().players
                .Concat(GameObject.FindGameObjectsWithTag("Damsel"))
                .ToArray();
        if (GetComponent<EnemyStats>().players.Length > 0)
        {
            FindClosest();

            //just put this here since it was erroring
            if (!GetComponent<EnemyHealth>())
            {
                return;
            }

            if (GetComponent<EnemyHealth>().canMove == false)
            {
                return;
            }
            if (closestdist > distanceFromPlayerToStop)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, GetComponent<EnemyStats>().closest.transform.position, step);
                //combatState = 0;
                GetComponent<EnemyStats>().combatState = 0;
            }
            else if (closestdist < distanceFromPlayerToRetreat)
            {
                //If the enemy is supposed to back up from the player, we do that here
                //When backing up, the enemy does not move at their full speed
                float step = (speed * 0.10f) * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, GetComponent<EnemyStats>().closest.transform.position, -step);
                GetComponent<EnemyStats>().combatState = 1;
                //combatState = 1;
            }
            else
            {
                //combatState = 2;
                GetComponent<EnemyStats>().combatState = 2;
            }

            MovementCheck();
        }

    }

    public void MovementCheck()
    {
        Vector3 tempVect = this.gameObject.transform.position;
        if (tempVect.x < 1)
        {
            tempVect.x = 1;
        }

        if (tempVect.y < 1)
        {
            tempVect.y = 1;
        }

        if (tempVect.x > GameMaster.xBord)
        {
            tempVect.x = GameMaster.xBord;
        }

        if (tempVect.y > GameMaster.yBord)
        {
            tempVect.y = GameMaster.yBord;
        }

        this.gameObject.transform.position = tempVect;
    }

    public void FindClosest()
    {
        closestdist = 9999;
        oldDistance = 9999;

        foreach (GameObject g in GetComponent<EnemyStats>().players)
        {
            float dist =
                Vector3.Distance(this.transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                GetComponent<EnemyStats>().closest = g;
                closestdist = dist;
                oldDistance = dist;
            }
        }
    }
}
