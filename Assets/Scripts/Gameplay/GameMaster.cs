using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviourPunCallbacks
{
    public GameObject OwnerUI;
    bool spawn = false;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.CreateRoom("hi", new RoomOptions() { MaxPlayers = 4, BroadcastPropsChangeToAll = true });
            Debug.Log("Connected to Local server!");
        }
        GameObject go = PhotonNetwork.Instantiate("Player", this.transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            OwnerUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        OwnerUI.SetActive(false);
        spawn = true;
        Debug.Log("Game Started!");
    }
}
