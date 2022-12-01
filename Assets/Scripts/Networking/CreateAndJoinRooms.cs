using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks 
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    //public TMP_InputField offlineInput;

    public static string RoomId = "";

    public void CreateRoom()
    {

        RoomOptions newRoomOptions = new RoomOptions();
        newRoomOptions.MaxPlayers = 4;

        RoomId = createInput.text;
        PhotonNetwork.CreateRoom(createInput.text, newRoomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gametest");
    }

    //public void CreateOfflineRoom()
    //{
    //    PhotonNetwork.OfflineMode = true;
    //    PhotonNetwork.CreateRoom(offlineInput.text);
    //}

}
