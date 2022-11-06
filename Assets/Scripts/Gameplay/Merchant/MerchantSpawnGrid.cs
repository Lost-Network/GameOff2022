using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MerchantSpawnGrid : MonoBehaviour
{
    public Transform[] spawnPoints;

    public string objectToSpawn;

    public string objectToSpawn2;

    void Awake()
    {
        // MerchantDialogueBox = GameObject.Find("MerchantDialogueBox");
    }

    void Start()
    {
        // MerchantDialogueBox.SetActive(textBox);
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
    }

    public void SpawnMerchant()
    {
        {
            int randMerchantSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randDamselSpawnPoint = Random.Range(0, spawnPoints.Length);
            while (randMerchantSpawnPoint == randDamselSpawnPoint)
            {
                randMerchantSpawnPoint = Random.Range(0, spawnPoints.Length);
                randDamselSpawnPoint = Random.Range(0, spawnPoints.Length);
            }

            GameObject go =
                PhotonNetwork
                    .Instantiate(objectToSpawn,
                    spawnPoints[randMerchantSpawnPoint].position,
                    transform.rotation);
            GameObject go2 =
                PhotonNetwork
                    .Instantiate(objectToSpawn2,
                    spawnPoints[randDamselSpawnPoint].position,
                    transform.rotation);
        }
    }
}
