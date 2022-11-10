using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    //We can alter these with difficulty settings or something
    public int damage = 1;
    [SerializeField]
    private float moveSpeed = 1;
    public int combatState = 0;
    public GameObject[] players;
    public GameObject closest;

    [Tooltip("Cost of spawning this enemy")]
    public int difficulty = 1;

    [Tooltip("The color of this enemy")]
    public Color enemyColor;


    public void Start()
    {
        enemyColor = this.GetComponent<SpriteRenderer>().color;
    }
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

    public void SetDefaultColor()
    {
        this.GetComponent<SpriteRenderer>().color = enemyColor;
    }
    public void DamageColorFlash()
    {
        this.GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);
        Invoke("SetDefaultColor", 0.08f);
    }

}
