using Photon.Pun;
using UnityEngine;

public class EnemyAttackProjectileSpiralPattern : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Name of object we are spawning")]
    private string objectToSpawn;

    [SerializeField]
    [Tooltip("How fast we are launching the object")]
    private float launchForce = 10f;

    [SerializeField]
    [Tooltip("Time before next volly")]
    private float objectSpawnTimer = 0f;

    [SerializeField]
    [Tooltip("How often we spawn something")]
    private float objectSpawnTimerCap = 3f;

    [SerializeField]
    [Tooltip("Time between each shot in the current spiral")]
    private float spiralDelay = 0;

    [SerializeField]
    [Tooltip("Time between each shot in the current spiral")]
    private float spiralDelayCap = 0.2f;

    [SerializeField]
    [Tooltip("The current step in the currently active spiral")]
    private int spiralProgress = 0;

    [SerializeField]
    [Tooltip("Fire counter-clockwise instead?")]
    private bool counterClockwise = false;

    [SerializeField]
    [Tooltip("Alternate direction?")]
    private bool alternateDirection = false;

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
        //We count up to being able to shoot the next volly
        if (objectSpawnTimer < objectSpawnTimerCap)
        {
            objectSpawnTimer += Time.deltaTime;
        }
        else
        {
            //We count up to being able to shoot the next shot in the same volly
            if (spiralDelay < spiralDelayCap)
            {
                spiralDelay += Time.deltaTime;
            }
            else
            {
                if (GetComponent<EnemyHealth>().canMove == false)
                {
                    return;
                }
                if (combatStateZeroAttack == true && GetComponent<EnemyStats>().combatState == 0 || combatStateOneAttack == true && GetComponent<EnemyStats>().combatState == 1 || combatStateTwoAttack == true && GetComponent<EnemyStats>().combatState == 2)
                {
                    if (counterClockwise == true)
                    {
                        //We let the next shot in this volly start counting
                        spiralDelay = 0;
                        Vector3 rot = new(0, 0, 45);
                        GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                        spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                        switch (spiralProgress)
                        {
                            case 0:
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 1:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 2:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 3:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 4:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 5:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 6:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 7:
                                spawnedObject.transform.Rotate(rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress = 0;
                                objectSpawnTimer = 0f;
                                if (alternateDirection == true)
                                {
                                    if (counterClockwise == true)
                                    {
                                        counterClockwise = false;
                                    }
                                    else
                                    {
                                        counterClockwise = true;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        spiralDelay = 0;
                        Vector3 rot = new(0, 0, 45);
                        GameObject spawnedObject = PhotonNetwork.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
                        spawnedObject.GetComponent<AttackStats>().damage = GetComponent<EnemyStats>().GetEnemyDamage();
                        switch (spiralProgress)
                        {
                            case 0:
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 1:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 2:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 3:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 4:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 5:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 6:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress++;
                                break;

                            case 7:
                                spawnedObject.transform.Rotate(-rot * spiralProgress);
                                spawnedObject.GetComponent<Rigidbody2D>().AddForce(spawnedObject.transform.up * launchForce);
                                spiralProgress = 0;
                                objectSpawnTimer = 0f;
                                if (alternateDirection == true)
                                {
                                    if (counterClockwise == true)
                                    {
                                        counterClockwise = false;
                                    }
                                    else
                                    {
                                        counterClockwise = true;
                                    }
                                }
                                break;
                        }
                    }

                }
            }
        }
    }
}