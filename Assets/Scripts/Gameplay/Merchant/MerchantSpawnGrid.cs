using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MerchantSpawnGrid : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] merchPrefabs;

    public GameObject MerchantDialogueBox;

    public string objectToSpawn;

    public bool textBox = false;

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
        if (Input.GetKeyDown("m"))
        {
            int merch = Random.Range(0, merchPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject go =
                PhotonNetwork
                    .Instantiate(objectToSpawn,
                    spawnPoints[randSpawnPoint].position,
                    transform.rotation);
        }
        if (Input.GetKeyDown("f"))
        {
            textBox = !textBox;
            MerchantDialogueBox.SetActive (textBox);
        }
    }

    void SpawnMerchant()
    {
    }
}
