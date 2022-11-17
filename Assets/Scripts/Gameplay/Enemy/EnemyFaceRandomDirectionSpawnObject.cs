using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyFaceRandomDirectionSpawnObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Name of object we are spawning")]
    private string objectToSpawn;
    [SerializeField]
    [Tooltip("How fast we are launching the object")]
    private float launchForce = 10f;
    [SerializeField]
    private float objectSpawnTimer = 0f;
    [SerializeField]
    [Tooltip("How often we spawn something")]
    private float objectSpawnTimerCap = 3f;
    [SerializeField]
    [Tooltip("Can we attack during combatState 0?")]
    private bool combatStateZeroAttack = false;
    [SerializeField]
    [Tooltip("Can we attack during combatState 1?")]
    private bool combatStateOneAttack = false;
    [SerializeField]
    [Tooltip("Can we attack during combatState 2?")]
    private bool combatStateTwoAttack = true;


    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if (GetComponent<EnemyHealth>().canMove == false)
            {
                return;
            }
            if (objectSpawnTimer >= objectSpawnTimerCap)
            {
                if (combatStateZeroAttack == true && GetComponent<EnemyStats>().combatState == 0 || combatStateOneAttack == true && GetComponent<EnemyStats>().combatState == 1 || combatStateTwoAttack == true && GetComponent<EnemyStats>().combatState == 2)
                {
                    objectSpawnTimer = 0f;
                    GetComponent<Rigidbody2D>().SetRotation(Random.Range(-180,181));
                    GetComponent<EnemyStats>().PlayProjectileTellOverNetwork();
                    Invoke("SpawnObject", 0.5f);
                }
            }
            else
            {
                objectSpawnTimer += Time.deltaTime;
            }
        }
    }
    private void SpawnObject()
    {
        GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
        //AttackStats needs to be on everything we spawn this way
        spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
        spawnedObject.GetComponent<Rigidbody2D>().AddForce(transform.up * -launchForce);
    }
}
