using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NodeManager : MonoBehaviourPunCallbacks
{
    public int xPos = 0;
    public int yPos = 0;
    public int xPosMax = 0;
    public int yPosMax = 0;
    public GameObject[] walls; //0 N, 1 E, 2 S, 3 W
    public int TileID = 0;

    public void setBounds()
    {
        bool a = true;
        bool b = true;
        bool c = true;
        bool d = true;
        //y sides
        if (yPos != (yPosMax-1))
        {
            walls[0].SetActive(false);
            a = false;
        }
        if (yPos != 0)
        {
            walls[2].SetActive(false);
            c = false;
        }

        //xPos sides
        if (xPos != (xPosMax - 1))
        {
            walls[1].SetActive(false);
            b = false;
        }
        if (xPos != 0)
        {
            walls[3].SetActive(false);
            d = false;
        }
        photonView.RPC("RemoveWalls", RpcTarget.AllBuffered,a,b,c,d);

    }

    [PunRPC]
    public void RemoveWalls(bool a, bool b, bool c, bool d)
    {
        walls[0].SetActive(a);
        walls[1].SetActive(b);
        walls[2].SetActive(c);
        walls[3].SetActive(d);
    }
}
