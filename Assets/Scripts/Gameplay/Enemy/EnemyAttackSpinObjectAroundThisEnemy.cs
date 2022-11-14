using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAttackSpinObjectAroundThisEnemy : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    [Tooltip("Name of object we are spawning, and having spin around this enemy")]
    private string objectToSpawn;
    [SerializeField]
    [Tooltip("How fast we are having this object spin")]
    private float spinSpeed = 80f;
    [SerializeField]
    [Tooltip("Timer for spawning object")]
    private float objectSpawnTimer = 0f;
    [SerializeField]
    [Tooltip("How often we spawn the spinning object")]
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
    [SerializeField]
    [Tooltip("Do we have an object spinning?")]
    private bool objectSpining = false;
    private GameObject currentObject;
    [SerializeField]
    [Tooltip("Timer for spinning the object")]
    private float spinDuration = 0f;
    [SerializeField]
    [Tooltip("How long we keep the object spinning around this enemy")]
    private float spinDurationCap = 1f;




    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            if(objectSpawnTimer < objectSpawnTimerCap && objectSpining == false)
            {
                objectSpawnTimer += Time.deltaTime;
            }
            else if(objectSpining == false)
            {
                if (combatStateZeroAttack == true && GetComponent<EnemyStats>().combatState == 0 || combatStateOneAttack == true && GetComponent<EnemyStats>().combatState == 1 || combatStateTwoAttack == true && GetComponent<EnemyStats>().combatState == 2)
                {
                    GetComponent<EnemyStats>().PlayProjectileTellOverNetwork();
                    objectSpining = true;
                    objectSpawnTimer = 0f;
                    Invoke("SpawnObject", 0.5f);
                }
            }
            if(objectSpining == true)
            {
                if(spinDuration < spinDurationCap && currentObject != null)
                {
                    currentObject.transform.RotateAround(transform.position, Vector3.forward, spinSpeed * Time.deltaTime);
                    spinDuration += Time.deltaTime;
                }
                else if(currentObject != null)
                {
                    spinDuration = 0;
                    objectSpining = false;
                    photonView.RPC("DestroyMe", RpcTarget.MasterClient);
                }
            }
        }


    }

    private void SpawnObject()
    {
        currentObject = PhotonNetwork.Instantiate(objectToSpawn, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
        currentObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
    }
    [PunRPC]
    void DestroyMe()
    {
        if (GetComponent<PhotonView>().AmOwner)
        {
            PhotonNetwork.Destroy(currentObject);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }




}
