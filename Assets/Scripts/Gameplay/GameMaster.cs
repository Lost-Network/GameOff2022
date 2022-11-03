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
    float amttoSpawn = 0;
    int wave = 0;
    float waveTimer = 5;
    float timer = 0;

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

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient || spawn == false)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer<= 0)
        {
            spawnWave();
        }


    }

    public void startGame()
    {
        OwnerUI.SetActive(false);
        spawn = true;
        Debug.Log("Game Started!");
        spawnWave();
    }

    public void spawnWave()
    {
        wave++;
        for (int i = 0; i < wave; i++)
        {
            float randomNumberX = Random.Range(1, 10);
            if (randomNumberX < 5) randomNumberX = randomNumberX - 10;
            float randomNumberY = Random.Range(1, 10);
            if (randomNumberY < 5) randomNumberY = randomNumberY - 10;
            Vector3 tempVect = new Vector3(randomNumberX, randomNumberY, 0);
            GameObject go = PhotonNetwork.Instantiate("Sus", tempVect, Quaternion.identity);
            timer = waveTimer * wave;
            if (timer >= 15) timer = 15;
        }
    }
}
