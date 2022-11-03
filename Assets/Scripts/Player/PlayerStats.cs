using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviourPunCallbacks, IPunObservable
{
    //Stuff related to HP
    [SerializeField]
    private int playerHealth = 10;
    [SerializeField]
    private int playerHealthMax = 10;
    private bool playerInvuln = false;
    private float invulnTimer = 0f;
    [SerializeField]
    private float invulnTimerCap = 1f;

    public Color playerColor;

    //Influences what the player can do
    //0 = alive/default
    //1 = dead/down
    //2 = ???
    [SerializeField]
    private int playerState = 0;

    //Stats


    //MONEY
    [SerializeField]
    static int money;
    

    //Call this to increase health
    public void IncreaseHealth(int healAmount)
    {
        if (GetComponent<PhotonView>().AmOwner && playerHealth != 1)
        {
            playerHealth += healAmount;
            playerHealth = Mathf.Clamp(playerHealth, 0, playerHealthMax);
        }
    }
    //Call this to decrease health
    public void DecreaseHealth(int damageAmount)
    {
        if (playerInvuln == false && GetComponent<PhotonView>().AmOwner && playerHealth != 1)
        {
            playerInvuln = true;
            GetComponent<SpriteRenderer>().color = new Color(playerColor.r, playerColor.g, playerColor.b, playerColor.a / 2);
            playerHealth -= damageAmount;
            playerHealth = Mathf.Clamp(playerHealth, 0, playerHealthMax);
            DeathCheck();
        }
    }

    //We run this each time we take damage
    private void DeathCheck()
    {
        if(playerHealth == 0)
        {
            playerState = 1;
            Debug.Log("Player is dead!");
        }
    }

    public int CheckifDead()
    {
        return playerState;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerHealth);
            stream.SendNext(playerState);
        }
        else
        {
            this.playerHealth = (int)stream.ReceiveNext();
            this.playerState = (int)stream.ReceiveNext();
        }
    }

    public void SpendMoney(int SpentMoney)
    {
        money -= SpentMoney;
    }

    public void GainMoney(int GainedMoney)
    {
        money -= GainedMoney;
    }

    private void FixedUpdate()
    {
        if(playerInvuln == true && invulnTimer >= invulnTimerCap)
        {
            playerInvuln = false;
            GetComponent<SpriteRenderer>().color = new Color(playerColor.r, playerColor.g, playerColor.b, playerColor.a);
            invulnTimer = 0f;
        }
        else
        {
            invulnTimer += Time.deltaTime;
        }        
    }
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Backspace) && Application.isEditor)
        {
            HurtPlayerDebug();
        }
    }

    //Set player color
    public void SetPlayerColor()
    {
        GetComponent<SpriteRenderer>().color = playerColor;
    }


    //Test kill player
    public void HurtPlayerDebug()
    {
        DecreaseHealth(1);
    }
}
