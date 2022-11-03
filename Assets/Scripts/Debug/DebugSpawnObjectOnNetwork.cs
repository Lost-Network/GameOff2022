using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawnObjectOnNetwork : MonoBehaviour
{
    public float spawnTimer = 0f;
    public float spawnTimerCap = 10f;
    public string objectToSpawn;
    private bool didWeSpawnSomething = false;

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if(spawnTimer < spawnTimerCap)
            {
                spawnTimer += Time.deltaTime;
            }
            else if (didWeSpawnSomething == false)
            {
                didWeSpawnSomething = true;
                GameObject go = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
            }
        }
    }
}
