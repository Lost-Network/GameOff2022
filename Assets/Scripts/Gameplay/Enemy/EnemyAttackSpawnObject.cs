using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAttackSpawnObject : MonoBehaviourPunCallbacks, IPunObservable
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

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        objectSpawnTimer -= GetComponent<EnemyStats>().attackSpeedFraction;
    }

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
                if(combatStateZeroAttack == true && GetComponent<EnemyStats>().combatState == 0 || combatStateOneAttack == true && GetComponent<EnemyStats>().combatState == 1 || combatStateTwoAttack == true && GetComponent<EnemyStats>().combatState == 2)
                {
                    objectSpawnTimer = 0f;
                    GetComponent<EnemyStats>().PlayProjectileTellOverNetwork();
                    Invoke("SpawnObject", 0.3f);
                }
            }
            else
            {
                objectSpawnTimer += Time.deltaTime;
            }
        }
    }
    private void RotateSpawnedObject(GameObject objectToRotate)
    {
        Vector3 targ = GetComponent<EnemyStats>().closest.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void SpawnObject()
    {
        GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
        //AttackStats needs to be on everything we spawn this way
        spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
        RotateSpawnedObject(spawnedObject);
        spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.right * launchForce);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
