using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviourPunCallbacks, IPunObservable
{
    //Stuff related to HP
    public int playerHealth = 10;
    public Image healthBar;

    public int playerHealthMax = 10;

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

    //Player Attack
    public int playerAtk = 1;

    //Stats
    //MONEY
    public static int money;

    //Damsel and Merchant
    public GameObject MerchantDialogueBox;

    public GameObject DamselDialogueBox;

    public bool deadMerchantCheck = false;

    public bool deadDamselCheck = false;

    public float time = 0f;

    void Awake()
    {
        MerchantDialogueBox = GameObject.Find("MerchantDialogueBox");
        DamselDialogueBox = GameObject.Find("DamselDialogueBox");
        //healthBar.GetComponent<MeshRenderer>().
    }

    //Call this to increase health
    public void IncreaseHealth(int healAmount)
    {
        if (GetComponent<PhotonView>().AmOwner && CheckPlayerState() != 1)
        {
            playerHealth += healAmount;
            playerHealth = Mathf.Clamp(playerHealth, 0, playerHealthMax);
            SetHealthBarFillVisual();
        }
    }

    //Call this to decrease health
    public void DecreaseHealth(int damageAmount)
    {
        if (
            playerInvuln == false &&
            GetComponent<PhotonView>().AmOwner &&
            CheckPlayerState() != 1
        )
        {
            playerInvuln = true;
            GetComponent<SpriteRenderer>().color =
                new Color(playerColor.r,
                    playerColor.g,
                    playerColor.b,
                    playerColor.a / 2);
            photonView.RPC("SetInvulnColor", RpcTarget.AllBuffered);
            playerHealth -= damageAmount;
            SetHealthBarFillVisual();
            DeathCheck();
        }
    }

    //We run this each time we take damage
    private void DeathCheck()
    {
        if (playerHealth == 0)
        {
            playerState = 1;
            GetComponent<SpriteRenderer>().color =
                new Color(200,
                    playerColor.g - 100,
                    playerColor.b - 100,
                    playerColor.a);
            photonView.RPC("SetDeadColor", RpcTarget.AllBuffered);
            Debug.Log("Player is dead!");
            if (gameObject.name.Contains("Merchant"))
            {
                MerchantDialogueBox
                    .GetComponent<MerchantSetActive>()
                    .ActivateMerchantDeath();
                deadMerchantCheck = true;
                time = 0f;
            }
            else if (gameObject.name.Contains("Damsel"))
            {
                DamselDialogueBox
                    .GetComponent<DamselSetActive>()
                    .ActivateDamselDeath();
                deadDamselCheck = true;
                time = 0f;
            }
        }
    }

    public int CheckPlayerState()
    {
        return playerState;
    }

    public void OnPhotonSerializeView(
        PhotonStream stream,
        PhotonMessageInfo info
    )
    {
        if (stream.IsWriting)
        {
            stream.SendNext (playerHealth);
            stream.SendNext (playerState);
            stream.SendNext (playerAtk);
        }
        else
        {
            this.playerHealth = (int) stream.ReceiveNext();
            this.playerState = (int) stream.ReceiveNext();
            this.playerAtk = (int) stream.ReceiveNext();
        }
    }

    public void SpendMoney(int SpentMoney)
    {
        money -= SpentMoney;
        Debug.Log("Money = " + money);
    }

    public void GainMoney(int GainedMoney)
    {
        money += GainedMoney;
        Debug.Log("Money = " + money);
    }

    private void FixedUpdate()
    {
        if (
            playerInvuln == true &&
            invulnTimer >= invulnTimerCap &&
            CheckPlayerState() != 1
        )
        {
            playerInvuln = false;
            GetComponent<SpriteRenderer>().color = playerColor;
            photonView
                .RPC("ResetPlayerColorBackToChosenColor",
                RpcTarget.AllBuffered);
            invulnTimer = 0f;
        }
        else if (playerInvuln == true)
        {
            invulnTimer += Time.deltaTime;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (deadMerchantCheck && time > 5)
        {
            Destroy (gameObject);
        }
        if (deadDamselCheck && time > 5)
        {
            Destroy (gameObject);
        }
        if (
            UnityEngine.Input.GetKeyDown(KeyCode.Backspace) &&
            Application.isEditor
        )
        {
            HurtPlayerDebug();
        }
    }

    //Set player color to the color they set, if this is a thing we ever add
    public void SetPlayerColor()
    {
        GetComponent<SpriteRenderer>().color = playerColor;
    }

    //Functions for setting the various color states for players over the network
    //Set player is dead color on network
    [PunRPC]
    public void SetDeadColor()
    {
        GetComponent<SpriteRenderer>().color =
            new Color(200,
                playerColor.g - 100,
                playerColor.b - 100,
                playerColor.a);
    }

    //Set player has invuln color on network
    [PunRPC]
    public void SetInvulnColor()
    {
        GetComponent<SpriteRenderer>().color =
            new Color(playerColor.r,
                playerColor.g,
                playerColor.b,
                playerColor.a / 2);
    }

    //Reset players color back to their chosen color on network
    [PunRPC]
    public void ResetPlayerColorBackToChosenColor()
    {
        GetComponent<SpriteRenderer>().color = playerColor;
    }
    private void SetHealthBarFillVisual()
    {
        float hpPercent = playerHealth * 0.1f;
        healthBar.fillAmount = hpPercent;
        photonView.RPC("SetHealthBarStatusOnNetwork", RpcTarget.AllBuffered, hpPercent);
    }
    [PunRPC]
    public void SetHealthBarStatusOnNetwork(float health)
    {
        healthBar.fillAmount = health;
    }

    //Test kill player
    public void HurtPlayerDebug()
    {
        DecreaseHealth(1);
    }
}
