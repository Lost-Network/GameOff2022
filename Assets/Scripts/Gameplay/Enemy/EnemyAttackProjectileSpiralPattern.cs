using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAttackProjectileSpiralPattern : MonoBehaviour
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


    private void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if(objectSpawnTimer < objectSpawnTimerCap)
        {
            objectSpawnTimer += Time.deltaTime;
        }
        else
        {
            if(spiralDelay < spiralDelayCap)
            {
                spiralDelay += Time.deltaTime;
            }
            else
            {
                if(GetComponent<EnemyStats>().combatState == 2 && counterClockwise == true)
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
                            if(alternateDirection == true)
                            {
                                if(counterClockwise == true)
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
                else if(GetComponent<EnemyStats>().combatState == 2)
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
