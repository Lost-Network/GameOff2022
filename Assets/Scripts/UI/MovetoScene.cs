using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MovetoScene : MonoBehaviourPunCallbacks
{
    int roomnum = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void swapScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void swapSceneLeaveLobby(int i)
    {
        roomnum = i;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(roomnum);

    }

    public void OnDisconnectedFromMasterServer()
    {
        SceneManager.LoadScene(roomnum);
    }

    public void LeaveGame()
    {
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        if (PhotonNetwork.OfflineMode == false)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            PhotonNetwork.OfflineMode = false;
            SceneManager.LoadScene(3);
        }

    }

    public void ReturnToLobby()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.LoadLevel(6);
        }
    }


    public override void OnLeftRoom()
    {
        swapSceneLeaveLobby(3);
    }

    public void ReloadScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(scene.name);
            PhotonNetwork.LoadLevel(scene);
        }
    }


}