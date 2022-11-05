using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MerchantSpawnGrid : MonoBehaviour
{
    public Transform[] spawnPoints;

    public string objectToSpawn;

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
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject go =
                PhotonNetwork
                    .Instantiate(objectToSpawn,
                    spawnPoints[randSpawnPoint].position,
                    transform.rotation);
        }
    }
}
