using Photon.Pun;
using UnityEngine;

public class EnemyAttackSpawnObjectOnTarget : MonoBehaviour
{
    //This will be used to spawn something directly ontop of the target, object being spawned should have a delayed hitbox so it can be moved out of
    [SerializeField]
    [Tooltip("Name of object we are spawning, this should be something with a delayed hitbox so the player can move out of it")]
    private string objectToSpawn;

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
                    GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, GetComponent<EnemyStats>().closest.transform.position, Quaternion.identity);
                    //AttackStats needs to be on everything we spawn this way
                    spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                }
            }
            else
            {
                objectSpawnTimer += Time.deltaTime;
            }
        }
    }
}