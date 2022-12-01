using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelection : MonoBehaviour
{
    public GameObject ClassSelectionObject;
    public Transform Soundboard;
    
    public GameObject myPlayer;
    public GameObject GameMaster;
    public GameObject lobby;
    public GameObject chicheSpawn;

    void Awake()
    {
        Soundboard = GameObject.Find("Soundboard/SFX").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnPlayer(string selection)
    {
        myPlayer =
            PhotonNetwork
                .Instantiate(selection,
                this.transform.position,
                Quaternion.identity);
        myPlayer.GetComponent<Movement>().xBorder = (GameMaster.GetComponent<MapSpawner>().howWide * 10) - 1;
        myPlayer.GetComponent<Movement>().yBorder = (GameMaster.GetComponent<MapSpawner>().howTall * 10) - 1;
        GameMaster.GetComponent<GameMaster>().SetPlayer(myPlayer);
    }

    public void SelectKnight()
    {
        GameMaster.GetComponent<GameMaster>().selection = "Player";
        SpawnPlayer("Player");
        Selection();
    }

    public void SelectArcher()
    {
        GameMaster.GetComponent<GameMaster>().selection = "PlayerArcher";
        SpawnPlayer("PlayerArcher");
        Selection();
    }

    public void SelectGunner()
    {
        GameMaster.GetComponent<GameMaster>().selection = "PlayerGunner";
        SpawnPlayer("PlayerGunner");
        Selection();
    }

    public void Selection()
    {
        Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        ClassSelectionObject.SetActive(false);
        chicheSpawn.SetActive(true);
    }
}
