using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawnGrid : MonoBehaviour
{

    public Transform[] spawnPoints; 
    public GameObject[] merchPrefabs; 

    public GameObject MerchantDialogueBox;

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
      if (Input.GetKeyDown("m")){
        int merch = Random.Range(0, merchPrefabs.Length); 
        int randSpawnPoint = Random.Range(0, spawnPoints.Length); 
        Instantiate(merchPrefabs[merch], spawnPoints[randSpawnPoint].position, transform.rotation); 
      }
      if (Input.GetKeyDown("f")){
        textBox = !textBox;
        MerchantDialogueBox.SetActive(textBox);
      }
    }

    void SpawnMerchant() 
    {
    
    }
}
