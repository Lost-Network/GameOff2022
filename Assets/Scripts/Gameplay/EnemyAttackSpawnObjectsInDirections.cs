using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAttackSpawnObjectsInDirections : MonoBehaviour
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
    [Tooltip("Does this launch objects in a + shape?")]
    private bool plusShape = false;
    [SerializeField]
    [Tooltip("Does this launch objects in a X shape?")]
    private bool crossShape = false;

    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if (objectSpawnTimer >= objectSpawnTimerCap && GetComponent<EnemyStats>().combatState == 2)
            {
                objectSpawnTimer = 0f;
                if(plusShape == true)
                {
                    //right
                    GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.right * launchForce);
                    //left
                    spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.right * -launchForce);
                    //up
                    spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                    //down
                    spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * -launchForce);

                }
                if(crossShape == true)
                {
                    Vector3 rot = new(0, 0, 45);
                    //right
                    GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.transform.Rotate(rot); 
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.right * launchForce);
                    //left
                    spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.transform.Rotate(rot);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.right * -launchForce);
                    //up
                    spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.transform.Rotate(rot);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                    //down
                    spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                    spawnedObject.transform.Rotate(rot);
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                    spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * -launchForce);
                }
            }
            else
            {
                objectSpawnTimer += Time.deltaTime;
            }
        }
    }
}
