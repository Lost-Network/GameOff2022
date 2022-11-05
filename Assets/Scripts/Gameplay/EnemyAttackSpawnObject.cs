using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAttackSpawnObject : MonoBehaviour
{
    [SerializeField]
    private string objectToSpawn;
    [SerializeField]
    private float launchForce = 10f;
    [SerializeField]
    private float objectSpawnTimer = 0f;
    [SerializeField]
    private float objectSpawnTimerCap = 3f;



    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if (objectSpawnTimer >= objectSpawnTimerCap && GetComponent<MovetowardsNearestPlayer>().combatState != 1)
            {
                objectSpawnTimer = 0f;
                GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                //AttackStats needs to be on everything we spawn this way
                spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                RotateSpawnedObject(spawnedObject);
                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.right * launchForce);
            }
            else
            {
                objectSpawnTimer += Time.deltaTime;
            }
        }
    }

    private void RotateSpawnedObject(GameObject objectToRotate)
    {
        Vector3 targ = GetComponent<MovetowardsNearestPlayer>().closest.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
