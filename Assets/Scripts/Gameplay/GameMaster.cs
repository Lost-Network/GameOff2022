using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviourPunCallbacks
{
    public GameObject OwnerUI;
    private bool spawn = false;
    private float amttoSpawn = 0;
    private int wave = 0;
    private float waveTimer = 5;
    private float timer = 0;
    public string[] enemies;
    public static GameObject myPlayer;

    public GameObject MerchantSpawner;

    public static int xBord = 0;
    public static int yBord = 0;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork
                .CreateRoom("hi",
                new RoomOptions()
                { MaxPlayers = 4, BroadcastPropsChangeToAll = true });
            Debug.Log("Connected to Local server!");
        }
        myPlayer =
            PhotonNetwork
                .Instantiate("Player",
                this.transform.position,
                Quaternion.identity);
        myPlayer.GetComponent<Movement>().xBorder = (this.GetComponent<MapSpawner>().howWide * 10) - 1;
        myPlayer.GetComponent<Movement>().yBorder = (this.GetComponent<MapSpawner>().howTall * 10) - 1;
        xBord = (this.GetComponent<MapSpawner>().howWide * 10) - 1;
        yBord = (this.GetComponent<MapSpawner>().howTall * 10) - 1;
        WaveManager.enemyCount = 0;
    }

    // Start is called before the first frame update
    private void Start()
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
        if (timer <= 0)
        {
            //spawnWave();
        }
    }

    public void startGame()
    {
        OwnerUI.SetActive(false);
        spawn = true;
        Debug.Log("Game Started!");
        this.GetComponent<MapSpawner>().setPos();
        //player location to tile from map spawner
        this.GetComponent<WaveManager>().spawnInitialWave();
        //MerchantSpawner.GetComponent<MerchantSpawnGrid>().SpawnMerchant();
    }

    public void spawnWave()
    {
        wave++;
        int counter = 0;
        for (int i = 0; i < wave; i++)
        {
            float randomNumberX = Random.Range(1, 10);
            if (randomNumberX < 5) randomNumberX = randomNumberX - 10;
            float randomNumberY = Random.Range(1, 10);
            if (randomNumberY < 5) randomNumberY = randomNumberY - 10;
            Vector3 tempVect = new Vector3(randomNumberX, randomNumberY, 0);
            GameObject ho = PhotonNetwork.Instantiate("Sus", tempVect, Quaternion.identity);
            GameObject go = PhotonNetwork.Instantiate(enemies[counter], tempVect, Quaternion.identity);
            counter++;
            if (counter > enemies.Length - 1)
            {
                counter = 0;
            }
            timer = waveTimer * wave;
            if (timer >= 15) timer = 15;
        }
    }
}