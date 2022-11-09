using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyPuddleDropper : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Name of object we are spawning")]
    private string objectToSpawn;

    [SerializeField]
    private float spawnTimer = 3f;
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
            if (timer > spawnTimer)
            {
                timer = 0;
                GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
            }
        }
    }
}
