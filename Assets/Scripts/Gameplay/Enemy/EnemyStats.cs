using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyStats : MonoBehaviourPunCallbacks, IPunObservable
{

    //We can alter these with difficulty settings or something
    public int damage = 1;
    [SerializeField]
    private float moveSpeed = 1;
    public int combatState = 0;
    public GameObject[] players;
    //increase this very slightly like 0.05 at a time or something, enemies will subtract their current attack speed by
    //this value
    public float attackSpeedFraction = 0;

    [Tooltip("This will throw errors if you use EnemyMovementRandomCompassDirection with EnemyMovementPivot or EnemyMovementStrafe, using those together will make the enemy move stupid anyways so just don't")]
    public GameObject closest;

    [Tooltip("Cost of spawning this enemy")]
    public int difficulty = 1;

    [Tooltip("Is this enemy a boss? Determines if we let this enemy get stunlocked by hitstun")]
    public bool isBoss = false;

    [Tooltip("The color of this enemy")]
    public Color enemyColor;

    public ParticleSystem projectileTell;


    public void Start()
    {
        //We stash the enemies color so we can restore it later
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

    //We restore the enemy color
    public void SetDefaultColor()
    {
        this.GetComponent<SpriteRenderer>().color = enemyColor;
    }
    //We make them flash red when they take damage
    public void DamageColorFlash()
    {
        this.GetComponent<SpriteRenderer>().color = new(255, 0, 0, 255);
        if(GetComponent<ParticleSystem>())
        {
            this.GetComponent<ParticleSystem>().Play();
        }    
        Invoke("SetDefaultColor", 0.08f);
    }

    [PunRPC]
    public void PlayProjectileTell()
    {
        if (projectileTell) projectileTell.Play();
    }
    public void PlayProjectileTellOverNetwork()
    {
        photonView.RPC("PlayProjectileTell", RpcTarget.AllBuffered);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

}
