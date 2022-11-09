using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SmallCheeseSpawner : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Name of object we are spawning")]
    private string objectToSpawn;

    [SerializeField]
    [Tooltip("Health to spawn object")]
    private int spawnHealth = 0;
    [SerializeField]
    private int spawnHealth2 = 0;

    [SerializeField]
    private float spawnTimer = 3f;
    [SerializeField]
    private float spawnTimer2 = 3f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if (timer > spawnTimer2 && GetComponent<EnemyHealth>().health < spawnHealth2)
            {
                timer = 0;
                GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
            }
            else if (timer > spawnTimer && GetComponent<EnemyHealth>().health < spawnHealth)
            {
                timer = 0;
                GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
            }
        }
    }
}
