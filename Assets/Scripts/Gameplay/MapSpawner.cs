using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSpawner : MonoBehaviourPunCallbacks
{
    public int howWide = 1;
    public int howTall = 1;
    public GameObject[] Map;
    public int startingTile = 0;

    // Start is called before the first frame update
    void Start()
    {
        Map = new GameObject[howTall * howWide];
        //Map = new GameObject[8];
        if (howWide < 1 || howTall < 1 || !PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Must be at least 1 wide and 1 tall");
            return;
        }
        int id = 0;
        for (int i = 0; i < howWide; i++)
        {
            for (int j = 0; j < howTall; j++)
            {
                Vector3 tempVect = new Vector3(5 + (10 * i), 5 + (10 * j), 0);
                GameObject go = PhotonNetwork.Instantiate("Node", tempVect, Quaternion.identity);
                go.GetComponent<NodeManager>().xPos = i;
                go.GetComponent<NodeManager>().yPos = j;
                go.GetComponent<NodeManager>().xPosMax = howWide;
                go.GetComponent<NodeManager>().yPosMax = howTall;
                go.GetComponent<NodeManager>().setBounds();
                go.GetComponent<NodeManager>().TileID = id;
                Map[id] = go;
                id++;
            }
        }
        startingTile = Random.Range(0, Map.Length);
        this.GetComponent<WaveManager>().spawnWave();
    }

    public void setPos()
    {
        float x = Map[startingTile].GetComponent<NodeManager>().xPos;
        float y = Map[startingTile].GetComponent<NodeManager>().yPos;
        photonView.RPC("movePlayers", RpcTarget.AllBuffered, x, y);
    }

    [PunRPC]
    public void movePlayers(float xLocation, float yLocation)
    {
        if (!GameMaster.myPlayer) return;
        GameObject p = GameMaster.myPlayer;
        Vector3 tempVect = new Vector3(5 + (10 * xLocation), 5 + (10 * yLocation), 0);
        p.transform.position = tempVect;
    }
}
