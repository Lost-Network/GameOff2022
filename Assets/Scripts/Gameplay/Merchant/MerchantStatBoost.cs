using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantStatBoost : MonoBehaviour
{
    public GameObject MerchantShop;
    public Transform Soundboard;

    public int attackBoost = 1;
    public int healthBoost = 2;
    public float speedBoost = 0.25f;

    void Awake()
    {
        Soundboard = GameObject.Find("Soundboard/SFX").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddAttackBoost()
    {
        Debug.Log("Added " + attackBoost + " attack!");
        GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk += attackBoost;
        Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        MerchantShop.SetActive(false);
    }

    public void AddHealthBoost()
    {
        Debug.Log("Added " + healthBoost + " health!");
        GameMaster.myPlayer.GetComponent<PlayerStats>().playerHealth += healthBoost;
        GameMaster.myPlayer.GetComponent<PlayerStats>().playerHealthMax += healthBoost;
        // GameMaster.myPlayer.GetComponent<PlayerStats>().IncreaseHealth(healthBoost);
        Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        MerchantShop.SetActive(false);
    }

    public void AddSpeedBoost()
    {
        Debug.Log("Added " + speedBoost + " speed!");
        GameMaster.myPlayer.GetComponent<Movement>().speed += speedBoost;
        Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
        MerchantShop.SetActive(false);
    }
}
