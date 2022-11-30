using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 offset;
    public GameObject player;

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(playerTrans.position.x + offset.x, playerTrans.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        }
        else
        {
            if (GameMaster.myPlayer != null)
            {
                player = GameMaster.myPlayer;
                playerTrans = player.transform;
            }
        }

    }
}
