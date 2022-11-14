using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantStatBoost : MonoBehaviour
{

    public GameObject MerchantShop;

    public Transform Soundboard;

    public int attackBoost = 1;
    public int healthBoost = 1;
    public int speedBoost = 1;

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

    public void AddAttackBoost(){
        Debug.Log("Added +1 attack!");
        GameMaster.myPlayer.GetComponent<PlayerStats>().playerAtk++;
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      MerchantShop.SetActive(false);
    }

    public void AddHealthBoost(){
      Debug.Log("Added +1 health!");
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      MerchantShop.SetActive(false);
    }

    public void AddSpeedBoost(){
      Debug.Log("Added +1 speed!");
      Soundboard.GetChild(0).GetComponent<AudioSource>().Play();
      MerchantShop.SetActive(false);
    }


}
