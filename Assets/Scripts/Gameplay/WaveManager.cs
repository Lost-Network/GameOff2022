using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class WaveManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public float remainingTime = 0;
    int waveCount = 0;
    public List<int> randomList = new List<int>();
    int interval = 0;
    bool waveActive = false;
    public static int enemyCount = 0;
    public int wave = 0;
    int specialSpawn = 0;
    int enemiesDisplayed = 0;
    public GameObject enemies;
    public GameObject timer;
    //public GameObject Victory;
    public GameObject NextWave;

    public GameObject TownShop;
    public bool shopped = true;
    public GameObject MerchantShop;
    public float shopTimer = 30f;

    public GameObject GameOverObject;

    public GameObject RoomIdObject;
    private string RoomId = "";

    public GameObject VictoryObject;
    public int BossNum = 0;
    public bool selection = true;
    bool eventCheck = true;
    public GameObject clicheSpawn;

    bool wonnered = false;
    public static GameObject GM;

    public GameObject victoryScreen;

    private void Start()
    {
        NextWave.SetActive(false);
        GameOverObject.SetActive(false);
        VictoryObject.SetActive(false);
        RoomId = CreateAndJoinRooms.RoomId;
        RoomIdObject.GetComponent<Text>().text = CreateAndJoinRooms.RoomId;
        GM = this.gameObject;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            NextWave.SetActive(false);
        }

        if (wonnered == true)
        {
            Victory();
            return;
        }

        if (enemiesDisplayed > 0)
        {
            enemies.SetActive(true);
            enemies.GetComponent<Text>().text = "Enemies: " + enemiesDisplayed.ToString();
        }
        else
        {
            enemies.SetActive(false);
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        enemiesDisplayed = enemyCount;

        if (enemyCount <= 0 && waveActive == true)
        {
            if (wave == 9)
            {
                Debug.Log("You Win!");
                wonnered = true;
            }

            VictoryObject.SetActive(true);
            enemies.GetComponent<Text>().text = "Shopping Phase";
            VictoryObject.GetComponent<Text>().text = "WAVE " + wave.ToString() + " CLEARED!";
            waveActive = false;
            shopped = false;
            if (wave == 3 || wave == 6)
            {
                selection = false;
                eventCheck = false;
                clicheSpawn.SetActive(true);
            }
            remainingTime = shopTimer;
            // spawnInitialWave();
        }


    }
    private void FixedUpdate()
    {
        if (wonnered) return;

        if (remainingTime > 0)
        {
            timer.gameObject.SetActive(true);
            int min = (int)remainingTime / 60;
            float second = remainingTime - (60 * min);
            if (second < 10)
            {
                timer.GetComponent<Text>().text = "Time: " + min.ToString() + ":0" + second.ToString("N1");
            }
            else
            {
                timer.GetComponent<Text>().text = "Time: " + min.ToString() + ":" + second.ToString("N1");
            }
        }
        else
        {
            timer.gameObject.SetActive(false);
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (remainingTime > 0 && waveActive == true)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                GameOver();
            }
        }
        else if (remainingTime > 0 && selection == false && waveActive == false)
        {
            remainingTime -= Time.deltaTime;

            if (eventCheck == false)
            {
                this.GetComponent<EventManager>().DisplayEventPage();
                eventCheck = true;
            }

            if (remainingTime <= shopTimer - 5 && shopped)
            {
                VictoryObject.SetActive(false);
                //NextWave.SetActive(false);
                shopped = false;
                TownShop.SetActive(false);
            }
            else if (remainingTime <= 0)
            {
                TownShop.SetActive(false);
                selection = true;
                this.GetComponent<EventManager>().submitID(0);
                spawnInitialWave();
            }
        }
        // Shop Timer
        else if (remainingTime > 0 && waveActive == false && shopped == false)
        {
            TownShop.SetActive(true);
            remainingTime -= Time.deltaTime;
            if (remainingTime <= shopTimer - 5 && shopped) {
                VictoryObject.SetActive(false);
                //NextWave.SetActive(true);
                shopped = false;
                TownShop.SetActive(true);
            }
            else if (remainingTime <= 0)
            {
                TownShop.SetActive(false);
                spawnInitialWave();
            }
        }
        else if (remainingTime <= 0 && wave>=1)
        {
            TownShop.SetActive(false);
            spawnInitialWave();
        }

        if (shopped == false || selection == false)
        {
            //NextWave.SetActive(false);
        }
    }

    public void spawnWave()
    {
        randomList = new List<int>();
        int size = this.GetComponent<MapSpawner>().howTall * this.GetComponent<MapSpawner>().howWide;
        for (int i = 0; i < size; i++)
        {
            randomList.Add(i);
        }
        randList();
        //add time 15s bonus time~ per square
    }

    public void spawnInitialWave()
    {

        VictoryObject.SetActive(false);
        NextWave.SetActive(false);
        shopped = true;
        waveActive = true;
        remainingTime = 120;
        randomList = new List<int>();
        int size = this.GetComponent<MapSpawner>().howTall * this.GetComponent<MapSpawner>().howWide;
        for (int i = 0; i < size; i++)
        {
            randomList.Add(i);
        }
        randList();
        wave++;
        specialSpawn = 0;
        //int counter = 1;
        if (wave == 3 || wave == 6 || wave == 9)
        {
            int pos = Random.Range(1, randomList.Count);
            Vector3 position = this.GetComponent<MapSpawner>().Map[randomList[pos]].transform.position;
            Vector3 tempVect = new Vector3(position.x, position.y, 0);
            GameObject bo = PhotonNetwork.Instantiate("Enemy/" + this.gameObject.GetComponent<GameMaster>().BossspawnList[BossNum].name, tempVect, Quaternion.identity);
            enemyCount++;
            BossNum++;
            return;
        }

        for (int i = 0; i < size; i++)
        {
            if (randomList[i] == this.GetComponent<MapSpawner>().startingTile)
            {
                continue;
            }

            Vector3 position = this.GetComponent<MapSpawner>().Map[randomList[i]].transform.position;
            //int counter = 1;
            for (int j = 0; j < wave; j++)
            {
                float randomNumberX = Random.Range(-4, 4);
                //if (randomNumberX < 5) randomNumberX = randomNumberX - 10;
                float randomNumberY = Random.Range(-4, 4);
                //if (randomNumberY < 5) randomNumberY = randomNumberY - 10;
                randomNumberX += position.x;
                randomNumberY += position.y;
                Vector3 tempVect = new Vector3(randomNumberX, randomNumberY, 0);
                //GameObject ho = PhotonNetwork.Instantiate("Sus", tempVect, Quaternion.identity);
                
                if (specialSpawn >= 5)
                {
                    int r = Random.Range(1, this.gameObject.GetComponent<GameMaster>().spawnList.Count);
                    specialSpawn = 0;
                    GameObject go = PhotonNetwork.Instantiate("Enemy/" + this.gameObject.GetComponent<GameMaster>().spawnList[r].name, tempVect, Quaternion.identity);
                    enemyCount++;
                    //counter++;
                    //if (counter > this.gameObject.GetComponent<GameMaster>().enemies.Length - 1)
                    //{
                    //    counter = 0;
                    //}
                }
                else
                {
                    specialSpawn++;
                    GameObject ho = PhotonNetwork.Instantiate("Sus", tempVect, Quaternion.identity);
                    enemyCount++;
                }
                


                //timer = waveTimer * wave;
                //if (timer >= 15) timer = 15;
            }

        }
        //add time 15s bonus time~ per square
    }

    public void randList()
    {
        for (int i = 0; i < randomList.Count; i++)
        {
            int temp = randomList[i];
            int randomIndex = Random.Range(i, randomList.Count);
            randomList[i] = randomList[randomIndex];
            randomList[randomIndex] = temp;
        }
    }

    public void GameOver()
    {
        GameOverObject.SetActive(true);
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
    }

    public void SetEnemies()
    {

    }


    public void OnPhotonSerializeView(
    PhotonStream stream,
    PhotonMessageInfo info
)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(enemiesDisplayed);
            stream.SendNext(remainingTime);
            stream.SendNext(wonnered);
        }
        else
        {
            this.enemiesDisplayed = (int)stream.ReceiveNext();
            this.remainingTime = (float)stream.ReceiveNext();
            this.wonnered = (bool)stream.ReceiveNext();

        }
    }

    public bool isMasterClient()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
