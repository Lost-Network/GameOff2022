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
    public GameObject[] enemies;
    public GameObject[] Bosses;
    public List<GameObject> spawnList = new List<GameObject>();
    public List<GameObject> BossspawnList = new List<GameObject>();
    public static GameObject myPlayer;

    public GameObject MerchantSpawner;

    public static int xBord = 0;
    public static int yBord = 0;

    public GameObject ClassSelection;
    public string selection = "Player";

    public void SetPlayer(GameObject Player)
    {
        myPlayer = Player;
    }

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
        else
        {
            spawnList.Add(enemies[0]);
            int a = Random.Range(0, Bosses.Length);
            int b = Random.Range(0, Bosses.Length);
            while (b == a)
            {
                b = Random.Range(0, Bosses.Length);
            }
            int c = Random.Range(0, Bosses.Length);
            while (c == a || c == b)
            {
                c = Random.Range(0, Bosses.Length);
            }
            BossspawnList.Add(Bosses[a]);
            BossspawnList.Add(Bosses[b]);
            BossspawnList.Add(Bosses[c]);
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

        myPlayer = ClassSelection.GetComponent<ClassSelection>().myPlayer;

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
            GameObject go = PhotonNetwork.Instantiate(enemies[counter].name, tempVect, Quaternion.identity);
            counter++;
            if (counter > enemies.Length - 1)
            {
                counter = 0;
            }
            timer = waveTimer * wave;
            if (timer >= 15) timer = 15;
        }
    }

    public void AddToSpawnList(int i)
    {
        spawnList.Add(enemies[i]);
    }
}