using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    //We can alter these with difficulty settings or something
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float moveSpeed = 1;
    public int combatState = 0;
    public GameObject[] players;
    public GameObject closest;



    public int GetEnemyDamage()
    {
        return damage;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void ChangeEnemyDamage(int value)
    {
        damage = value;
    }

    public void ChangeEnemyMoveSpeed(float value)
    {
        moveSpeed = value;
    }

}
